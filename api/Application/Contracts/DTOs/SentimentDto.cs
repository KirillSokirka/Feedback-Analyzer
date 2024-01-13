namespace FeedbackAnalyzer.Application.Contracts.DTOs;

public class SentimentDto
{
    public string Sentiment { get; set; } = null!;
    public double PositiveScore { get; set; }
    public double NeutralScore { get; set; }
    public double NegativeScore { get; set; }
}