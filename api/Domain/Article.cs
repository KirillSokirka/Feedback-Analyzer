using FeedbackAnalyzer.Domain.Common;

namespace FeedbackAnalyzer.Domain;

public class Article : BaseEntity
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required User Creator { get; set; }
    public required string CreatorId { get; set; }
    public List<Comment>? Comments { get; set; }
    public List<FeedbackSentiment>? Feedbacks { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
}