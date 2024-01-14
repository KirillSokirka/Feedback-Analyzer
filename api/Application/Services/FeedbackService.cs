using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Contracts.Services;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Domain;

namespace FeedbackAnalyzer.Application.Services;

public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackSentimentRepository _feedbackRepository;
    private readonly ITextAnalyticsService _analyticsService;
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public FeedbackService(IFeedbackSentimentRepository feedbackRepository, ITextAnalyticsService analyticsService,
        IMapper mapper, IArticleRepository articleRepository)
    {
        _feedbackRepository = feedbackRepository;
        _analyticsService = analyticsService;
        _mapper = mapper;
        _articleRepository = articleRepository;
    }
    
    public async Task<Result<SentimentDto>> GetArticleFeedbackAsync(Article article)
    {
        var feedback = (await _feedbackRepository
                .FindAsync(f => f.ArticleId == article.Id && f.Type == FeedbackType.Article))
            .FirstOrDefault();

        if (feedback is not null && article.Updated < feedback.Created)
        {
            return _mapper.Map<SentimentDto>(feedback);
        }

        var analysisResult = await _analyticsService.CreateSentimentAsync(article.Content);

        if (analysisResult.IsFailure)
        {
            return Result<SentimentDto>.Failure(analysisResult.Error);
        }

        feedback = _mapper.Map<FeedbackSentiment>(analysisResult.Value);

        feedback.SetArticleFeedback(article.Id);

        await _feedbackRepository.CreateAsync(feedback);

        return _mapper.Map<SentimentDto>(feedback);
    }
    
    public async Task<Result<SentimentDto?>> GetArticleCommentsFeedbackAsync(Article article)
    {
        if (article.Comments is null)
        {
            return null!;
        }
        
        var lastCreatedComment = article.Comments.MaxBy(c => c.Created);
        
        var feedback = (await _feedbackRepository
                .FindAsync(f => f.ArticleId == article.Id && f.Type == FeedbackType.ArticleComments))
                .FirstOrDefault();

        if (feedback is not null && feedback.Created > lastCreatedComment!.Created)
        {
            return _mapper.Map<SentimentDto>(feedback);
        }

        var analysisResult = await _analyticsService.CreateAverageSentimentAsync(article.Comments.Select(c => c.Text));

        if (analysisResult.IsFailure)
        {
            return Result<SentimentDto?>.Failure(analysisResult.Error);
        }

        feedback = _mapper.Map<FeedbackSentiment>(analysisResult.Value);

        feedback.SetArticleCommentsFeedback(article.Id);

        await _feedbackRepository.CreateAsync(feedback);

        return analysisResult!;
    }
    
    public async Task<Result<SentimentDto?>> GetUserArticlesFeedbackAsync(User user)
    {
        var userArticles = await _articleRepository.FindAsync(a => a.CreatorId == user.Id);

        if (!userArticles.Any())
        {
            return null!;
        }

        var lastArticleCreated = userArticles.MaxBy(a => a.Created);
        var lastArticleUpdated = userArticles.MaxBy(a => a.Updated);

        var lastArticleDate = lastArticleCreated!.Created > lastArticleUpdated!.Updated
            ? lastArticleCreated.Created
            : lastArticleUpdated.Updated;
        
        var feedback = (await _feedbackRepository
                .FindAsync(f => f.UserId == user.Id && f.Type == FeedbackType.UserArticles))
                .FirstOrDefault();

        if (feedback is not null && feedback.Created > lastArticleDate)
        {
            return _mapper.Map<SentimentDto>(feedback);
        }
        
        var analysisResult = await _analyticsService.CreateAverageSentimentAsync(userArticles.Select(c => c.Content));

        if (analysisResult.IsFailure)
        {
            return Result<SentimentDto?>.Failure(analysisResult.Error);
        }

        feedback = _mapper.Map<FeedbackSentiment>(analysisResult.Value);

        feedback.SetUserFeedback(user.Id);

        await _feedbackRepository.CreateAsync(feedback);

        return analysisResult!;
    }
}