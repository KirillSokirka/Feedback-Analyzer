using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Comment.DeleteComment;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result<Unit>>
{
    private readonly ICommentRepository _commentRepository;

    public DeleteCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
    public async Task<Result<Unit>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var article = await _commentRepository.GetByIdAsync(request.Id);

        if (article is null)
        {
            return Result<Unit>.Failure(CommentErrors.NotFound(request.Id));
        }

        await _commentRepository.DeleteAsync(article);

        return Unit.Value;
    }
}