using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.DeleteArticle;

public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Result<bool>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IFeedbackSentimentRepository _feedbackRepository;

    public DeleteArticleCommandHandler(IArticleRepository articleRepository,
        IFeedbackSentimentRepository feedbackRepository)
    {
        _articleRepository = articleRepository;
        _feedbackRepository = feedbackRepository;
    }

    public async Task<Result<bool>> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.Id);

        if (article is null)
        {
            return Result<bool>.Failure(ArticleErrors.NotFound(request.Id));
        }

        var commentsToDelete = await _feedbackRepository.FindAsync(c => c.ArticleId == article.Id);

        foreach (var comment in commentsToDelete)
        {
            await _feedbackRepository.DeleteAsync(comment);
        }

        await _articleRepository.DeleteAsync(article);

        return true;
    }
}