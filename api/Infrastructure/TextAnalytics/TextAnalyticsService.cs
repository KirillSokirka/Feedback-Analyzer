using Azure;
using Azure.AI.TextAnalytics;
using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Contracts.Services;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Domain;
using Microsoft.Extensions.Options;

namespace Infrastructure.TextAnalytics;

public class TextAnalyticsService : ITextAnalyticsService
{
    private readonly TextAnalyticsClient _analyticsClient;
    
    public TextAnalyticsService(IOptions<TextAnalyticsOptions> options)
    {
        _analyticsClient = new TextAnalyticsClient(options.Value.Endpoint, options.Value.SecretKey);
    }

    public async Task<Result<SentimentDto>> CreateAverageSentimentAsync(IEnumerable<string> texts)
    {
        Response<AnalyzeSentimentResultCollection> response;
        
        try
        {
            response = await _analyticsClient.AnalyzeSentimentBatchAsync(texts);
        }
        catch (RequestFailedException e)
        {
            return Result<SentimentDto>.Failure(Error.Failure(e.ErrorCode ?? "500", e.Message));
        }

        var results = response.Value;

        var averagePositiveScore =
            CalculateAverageConfidence(results.Select(x => x.DocumentSentiment.ConfidenceScores.Positive).ToList());
        var averageNeutralScore =
            CalculateAverageConfidence(results.Select(x => x.DocumentSentiment.ConfidenceScores.Neutral).ToList());
        var averageNegativeScore =
            CalculateAverageConfidence(results.Select(x => x.DocumentSentiment.ConfidenceScores.Negative).ToList());

        var overallSentiment =
            DetermineOverallSentiment(averagePositiveScore, averageNegativeScore, averageNeutralScore);

        return new SentimentDto
        {
            Sentiment = MapSentimentType(overallSentiment),
            PositiveScore = averagePositiveScore,
            NeutralScore = averageNeutralScore,
            NegativeScore = averageNegativeScore
        };
    }

    public async Task<Result<SentimentDto>> CreateSentimentAsync(string text)
    {
        Response<DocumentSentiment> response;
        
        try
        {
            response = await _analyticsClient.AnalyzeSentimentAsync(text);
        }
        catch (RequestFailedException e)
        {
            return Result<SentimentDto>.Failure(Error.Failure(e.ErrorCode ?? "500", e.Message));
        }
        
        var sentiment = response.Value;

        return new SentimentDto
        {
            Sentiment = MapSentimentType(sentiment.Sentiment.ToString()),
            PositiveScore = sentiment.ConfidenceScores.Positive,
            NeutralScore = sentiment.ConfidenceScores.Neutral,
            NegativeScore = sentiment.ConfidenceScores.Negative
        };
    }

    #region Private Methods

    private static string DetermineOverallSentiment(double positive, double negative,
        double neutral)
    {
        if (positive > neutral && positive > negative)
            return "Positive";

        return negative > neutral ? "Negative" : "Neutral";
    }

    private static double CalculateAverageConfidence(IReadOnlyCollection<double> scores)
        => scores.Count > 0 ? scores.Sum() / scores.Count : 0;

    private static SentimentType MapSentimentType(string sentiment)
        => sentiment switch
        {
            "Positive" => SentimentType.Positive,
            "Negative" => SentimentType.Negative,
            "Neutral" => SentimentType.Neutral,
            _ => SentimentType.Empty
        };

    #endregion
}