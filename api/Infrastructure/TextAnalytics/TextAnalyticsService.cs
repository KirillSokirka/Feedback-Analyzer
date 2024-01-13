using Azure.AI.TextAnalytics;
using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Contracts.Services;
using Microsoft.Extensions.Options;

namespace Infrastructure.TextAnalytics;

public class TextAnalyticsService : ITextAnalyticsService
{
    private readonly TextAnalyticsClient _analyticsClient;

    public TextAnalyticsService(IOptions<TextAnalyticsOptions> options)
    {
        _analyticsClient = new TextAnalyticsClient(options.Value.Endpoint, options.Value.SecretKey);
    }

    public async Task<SentimentDto> CreateAverageSentiment(IEnumerable<string> texts)
    {
        var response = await _analyticsClient.AnalyzeSentimentBatchAsync(texts);

        var results = response.Value;

        var averagePositiveScore =
            CalculateAverageConfidence(results.Select(x => x.DocumentSentiment.ConfidenceScores.Positive).ToList());
        var averageNeutralScore =
            CalculateAverageConfidence(results.Select(x => x.DocumentSentiment.ConfidenceScores.Neutral).ToList());
        var averageNegativeScore =
            CalculateAverageConfidence(results.Select(x => x.DocumentSentiment.ConfidenceScores.Negative).ToList());

        var overallSentiment =
            DetermineOverallSentiment(averagePositiveScore, averageNeutralScore, averageNegativeScore);

        return new SentimentDto
        {
            Sentiment = overallSentiment,
            PositiveScore = averagePositiveScore,
            NeutralScore = averageNeutralScore,
            NegativeScore = averageNegativeScore
        };
    }

    public async Task<SentimentDto> CreateSentiment(string text)
    {
        var response = await _analyticsClient.AnalyzeSentimentAsync(text);
        var sentiment = response.Value;

        return new SentimentDto
        {
            Sentiment = sentiment.Sentiment.ToString(),
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

    #endregion
}