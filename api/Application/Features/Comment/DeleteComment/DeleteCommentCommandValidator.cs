using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using FluentValidation;

namespace FeedbackAnalyzer.Application.Features.Comment.DeleteComment;

public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    private readonly ICommentRepository _commentRepository;
    
    public DeleteCommentCommandValidator(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
        
        RuleFor(x => x.Id)
            .MustAsync(CommentShouldExist)
            .WithState(c => CommentErrors.NotFound(c.Id));

        RuleFor(x => x)
            .MustAsync(CommentBelongsToArticle)
            .WithState(c => CommentErrors.CommentDoesNotBelongToArticle(c.Id, c.Id));
    }
    
    private async Task<bool> CommentShouldExist(string id, CancellationToken cancellationToken)
        => await _commentRepository.GetByIdAsync(id) is not null;

    private async Task<bool> CommentBelongsToArticle(DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(command.Id);
        
        return comment!.ArticleId == command.ArticleId;
    }
}