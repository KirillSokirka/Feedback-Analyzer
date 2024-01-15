using FeedbackAnalyzer.Domain;

namespace FeedbackAnalyzer.Application.Contracts.DTOs;

public class SentimentDto
{
    public SentimentType Sentiment { get; set; } = SentimentType.Empty;
    public double PositiveScore { get; set; }
    public double NeutralScore { get; set; }
    public double NegativeScore { get; set; }

    public static SentimentDto Empty() => new();

}