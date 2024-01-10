namespace FeedbackAnalyzer.Domain.Common;

public abstract class BaseEntity
{
    public required string Id { get; set; } = Guid.NewGuid().ToString();
}