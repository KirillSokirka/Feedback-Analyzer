using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using FluentValidation;

namespace FeedbackAnalyzer.Application.Features.Comment.UpdateComment;

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IArticleRepository _articleRepository;

    public UpdateCommentCommandValidator(ICommentRepository commentRepository, IArticleRepository articleRepository)
    {
        _commentRepository = commentRepository;
        _articleRepository = articleRepository;

        RuleFor(p => p.Text)
            .NotEmpty()
            .WithState(_ => CommentErrors.NotValidComment())
            .MaximumLength(100)
            .WithState(_ => CommentErrors.NotValidComment());

        RuleFor(x => x.Id)
            .MustAsync(CommentShouldExist)
            .WithState(c => CommentErrors.NotFound(c.Id));

        RuleFor(x => x)
            .MustAsync(CommentBelongsToArticle)
            .WithState(c => CommentErrors.CommentDoesNotBelongToArticle(c.Id, c.ArticleId));
    }

    private async Task<bool> CommentShouldExist(string id, CancellationToken cancellationToken)
        => await _commentRepository.GetByIdAsync(id) is not null;

    private async Task<bool> CommentBelongsToArticle(UpdateCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(command.Id);
        
        return comment!.ArticleId == command.ArticleId;
    }
}
