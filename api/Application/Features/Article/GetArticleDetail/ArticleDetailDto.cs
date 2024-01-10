using FeedbackAnalyzer.Application.Features.Article.Queries.GetAllArticles;
using FeedbackAnalyzer.Application.Features.Comment.Queries;

namespace FeedbackAnalyzer.Application.Features.Article.GetArticleDetail;

public record ArticleDetailDto(
    string Id,
    string Title,
    string Content,
    UserDto Creator,
    List<CommentDto>? Comments,
    DateTime Created,
    DateTime Updated
);