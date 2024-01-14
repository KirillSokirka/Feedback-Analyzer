using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Contracts.Services;
using FeedbackAnalyzer.Application.Features.Comment.CreateCommentsFeedbackByArticle;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Feedback.CreateCommentsFeedbackByArticle;

public class
    CreateCommentsFeedbackCommandHandler : IRequestHandler<CreateCommentsFeedbackCommand, Result<SentimentDto?>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IFeedbackService _feedbackService;

    public CreateCommentsFeedbackCommandHandler(IArticleRepository articleRepository, IFeedbackService feedbackService)
    {
        _articleRepository = articleRepository;
        _feedbackService = feedbackService;
    }

    public async Task<Result<SentimentDto?>> Handle(CreateCommentsFeedbackCommand request,
        CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.ArticleId);

        if (article is null)
        {
            return Result<SentimentDto?>.Failure(ArticleErrors.NotFound(request.ArticleId));
        }

        var feedback = await _feedbackService.GetArticleCommentsFeedbackAsync(article);

        return feedback.IsSuccess ? feedback : Result<SentimentDto?>.Failure(feedback.Error);
    }
}