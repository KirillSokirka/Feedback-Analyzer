using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.CreateArticle;

public record CreateArticleCommand(string Title, string Content, string CreatorId) : IRequest<Result<string>>;
