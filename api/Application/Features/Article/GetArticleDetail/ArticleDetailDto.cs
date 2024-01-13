using FeedbackAnalyzer.Application.Features.Comment;
using FeedbackAnalyzer.Application.Features.User;

namespace FeedbackAnalyzer.Application.Features.Article.GetArticleDetail;

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