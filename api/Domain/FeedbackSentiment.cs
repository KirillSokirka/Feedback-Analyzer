using FeedbackAnalyzer.Domain.Common;

namespace FeedbackAnalyzer.Domain;

public class FeedbackSentiment : BaseEntity
{
    public string? ArticleId { get; private set; }
    public string? UserId { get; private set; }
    public FeedbackType Type { get; private set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public required FeedbackType Sentiment { get; set; }
    public double PositiveScore { get; set; }
    public double NeutralScore { get; set; }
    public double NegativeScore { get; set; }
    
    public void SetArticleFeedback(string articleId)
    {
        ArticleId = articleId;
        Type = FeedbackType.Article;
    }
    
    public void SetUserFeedback(string userId)
    {
        UserId = userId;
        Type = FeedbackType.UserArticles;
    }

    public void SetArticleCommentsFeedback(string? articleId)
    {
        ArticleId = articleId;
        Type = FeedbackType.ArticleComments;
    }
}

public enum FeedbackType
{
    Article = 0,
    ArticleComments = 1,
    UserArticles = 2
}

public enum SentimentType
{
    Positive = 0,
    Negative = 1,
    Neutral = 2,
    Empty = 4,
}