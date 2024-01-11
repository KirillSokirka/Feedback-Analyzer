using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.DeleteArticle;

public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Result<bool>>
{
    private readonly IArticleRepository _articleRepository;

    public DeleteArticleCommandHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<Result<bool>> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.Id);

        if (article is null)
        {
            return Result<bool>.Failure(ArticleErrors.NotFound(request.Id));
        }

        await _articleRepository.DeleteAsync(article);

        return true;
    }
}