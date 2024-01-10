namespace FeedbackAnalyzer.Application.Features.Article.GetAllArticles;

public record ArticleDto(
    string Id,
    string Title,
    string Creator,
    DateTime Created,
    DateTime Updated
);