using FeedbackAnalyzer.Domain.Common;

namespace FeedbackAnalyzer.Domain;

public class Comment : BaseEntity
{
    public required string Text { get; set; }
    
    public required string ArticleId { get; set; }
    public required Article Article { get; set; }
    
    public required string CommentatorId { get; set; }
    public required User Commentator { get; set; }
    
    public DateTime Created { get; set; }
}