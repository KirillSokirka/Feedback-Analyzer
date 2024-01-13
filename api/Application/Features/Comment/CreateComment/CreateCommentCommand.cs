using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Comment.CreateComment;

public record CreateCommentCommand(string Text, string CommentatorId, string ArticleId) : IRequest<Result<string>>;