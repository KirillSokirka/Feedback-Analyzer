using FeedbackAnalyzer.Domain.Common;

namespace FeedbackAnalyzer.Domain;

public class FeedbackSentiment : BaseEntity
{
    public string? ArticleId { get; private set; }
    public string? UserId { get; private set; }
    public FeedbackType Type { get; private set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public required string Sentiment { get; set; }
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
        Type = FeedbackType.UserComments;
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
    UserComments = 2
}