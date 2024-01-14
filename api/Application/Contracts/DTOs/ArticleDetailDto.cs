using FeedbackAnalyzer.Application.Features.Comment;

namespace FeedbackAnalyzer.Application.Contracts.DTOs;

public record ArticleDetailDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public UserDto Creator { get; set; }
    public List<CommentDto>? Comments { get; set; } = new();
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}