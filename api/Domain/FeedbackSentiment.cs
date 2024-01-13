using FeedbackAnalyzer.Domain.Common;

namespace FeedbackAnalyzer.Domain;

public class FeedbackSentiment : BaseEntity
{
    public string? ArticleId { get; set; }
    public string? UserId { get; set; }
    public FeedbackType Type { get; set; }
    public required string Sentiment { get; set; }
    public double PositiveScore { get; set; }
    public double NeutralScore { get; set; }
    public double NegativeScore { get; set; }
}

public enum FeedbackType
{
    Article = 0,
    ArticleComments = 1,
    UserComments = 2
}