using System.Windows.Input;
using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.DeleteArticle;

public record DeleteArticleCommand(string Id) : IRequest<Result<bool>>;
