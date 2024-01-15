using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using FluentValidation;

namespace FeedbackAnalyzer.Application.Features.Article.CreateArticle;

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateArticleCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        RuleFor(p => p.Title)
            .NotEmpty()
            .WithState(_ => ArticleErrors.NotValidTitle())
            .MaximumLength(50)
            .WithState(_ => ArticleErrors.NotValidTitle());

        RuleFor(p => p.Content)
            .NotEmpty()
            .WithState(_ => ArticleErrors.NotValidContent());

        RuleFor(p => p.CreatorId)
            .MustAsync(CreatorShouldExist)
            .WithState(command => ArticleErrors.ArticleCreatorNotFound(command.CreatorId));
    }

    private async Task<bool> CreatorShouldExist(string creatorId, CancellationToken ct)
        => (await _userRepository.FindAsync(u => u.IdentityId == creatorId)).FirstOrDefault() is not null;
}