using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using FluentValidation;

namespace FeedbackAnalyzer.Application.Features.Article.UpdateArticle;

public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
{
    private readonly IArticleRepository _articleRepository;
    
    public UpdateArticleCommandValidator(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;

        RuleFor(x => x.Id)
            .MustAsync(ArticleShouldExists)
            .WithState(a => ArticleErrors.NotFound(a.Id));

        When(x => x.Title != null, () =>
        {
            RuleFor(x => x.Title)
                .MaximumLength(50)       
                .WithState(_ => ArticleErrors.NotValidTitle());
        });
    }

    private async Task<bool> ArticleShouldExists(string id, CancellationToken cancellationToken)
        => await _articleRepository.GetByIdAsync(id) is not null;
}