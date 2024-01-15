using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using FluentValidation;

namespace FeedbackAnalyzer.Application.Features.Comment.CreateComment;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUserRepository _userRepository;

    public CreateCommentCommandValidator(IUserRepository userRepository, IArticleRepository articleRepository)
    {
        _userRepository = userRepository;
        _articleRepository = articleRepository;
        
        RuleFor(p => p.Text)
            .NotEmpty()
            .WithState(_ => CommentErrors.NotValidComment())
            .MaximumLength(100)
            .WithState(_ => CommentErrors.NotValidComment());

        RuleFor(x => x.ArticleId)
            .MustAsync(ArticleShouldExists)
            .WithState(c => CommentErrors.NotFoundArticle(c.ArticleId));
        
        RuleFor(x => x.CommentatorId)
            .MustAsync(CommentatorShouldExist)
            .WithState(c => CommentErrors.NotFoundCommentator(c.CommentatorId));
    }
    
    private async Task<bool> ArticleShouldExists(string id, CancellationToken cancellationToken)
        => await _articleRepository.GetByIdAsync(id) is not null;
    
    private async Task<bool> CommentatorShouldExist(string creatorId, CancellationToken ct)
        => (await _userRepository.FindAsync(u => u.IdentityId == creatorId)).FirstOrDefault() is not null;
}