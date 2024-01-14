using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.GetArticleDetail;

public record GetArticleDetailQuery(string Id) : IRequest<Result<ArticleDetailDto>>;