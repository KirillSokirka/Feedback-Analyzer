using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Comment.DeleteComment;

public record DeleteCommentCommand(string Id, string ArticleId) : IRequest<Result<Unit>>;