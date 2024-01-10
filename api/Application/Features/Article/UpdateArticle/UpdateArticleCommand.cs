using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.UpdateArticle;

public record UpdateArticleCommand(string Id, string? Title, string? Content) : IRequest<Result<string>>;
