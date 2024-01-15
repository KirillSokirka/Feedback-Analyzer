using FeedbackAnalyzer.Domain.Common;

namespace FeedbackAnalyzer.Domain;

public class User : BaseEntity
{
    public required string IdentityId { get; set; }
    public required string FullName { get; set; }
    public string? Rating { get; set; }
    public List<Article>? Articles { get; set; }
    public List<Comment>? LeavedComments { get; set; }
    public List<FeedbackSentiment>? Feedbacks { get; set; }
}