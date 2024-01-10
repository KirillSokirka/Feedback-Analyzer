using FeedbackAnalyzer.Domain.Common;

namespace FeedbackAnalyzer.Domain;

public class User : BaseEntity
{
    public required string Name { get; set; }
    public string? Rating { get; set; }
    public List<Article>? Articles { get; set; }
}