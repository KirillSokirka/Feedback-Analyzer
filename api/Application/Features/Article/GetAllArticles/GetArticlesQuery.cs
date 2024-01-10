using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.GetAllArticles;

public record GetArticlesQuery : IRequest<List<ArticleDto>>;