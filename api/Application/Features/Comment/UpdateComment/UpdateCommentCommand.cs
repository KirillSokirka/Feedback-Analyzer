using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Comment.UpdateComment;

public record UpdateCommentCommand(string Id, string Text, string ArticleId) : IRequest<Result<Unit>>;