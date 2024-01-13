using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.GetAllArticles;

public record GetArticlesQuery : IRequest<Result<List<ArticleDto>>>;