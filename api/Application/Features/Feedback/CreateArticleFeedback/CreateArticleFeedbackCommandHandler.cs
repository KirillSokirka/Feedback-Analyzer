using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Contracts.Services;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Feedback.CreateArticleFeedback;

public class CreateArticleFeedbackCommandHandler : IRequestHandler<CreateArticleFeedbackCommand, Result<SentimentDto>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IFeedbackService _feedbackService;

    public CreateArticleFeedbackCommandHandler(IArticleRepository articleRepository, IFeedbackService feedbackService)
    {
        _articleRepository = articleRepository;
        _feedbackService = feedbackService;
    }

    public async Task<Result<SentimentDto>> Handle(CreateArticleFeedbackCommand request,
        CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.ArticleId);

        if (article is null)
        {
            return Result<SentimentDto>.Failure(ArticleErrors.NotFound(request.ArticleId));
        }

        var feedback = await _feedbackService.GetArticleFeedbackAsync(article);

        return feedback.IsSuccess ? feedback : Result<SentimentDto>.Failure(feedback.Error);
    }
}