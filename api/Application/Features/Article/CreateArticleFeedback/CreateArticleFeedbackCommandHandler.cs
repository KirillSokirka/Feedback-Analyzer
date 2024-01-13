using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Contracts.Services;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using FeedbackAnalyzer.Domain;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.CreateArticleFeedback;

public class CreateArticleFeedbackCommandHandler : IRequestHandler<CreateArticleFeedbackCommand, Result<SentimentDto>>
{
    private readonly ITextAnalyticsService _analyticsService;
    private readonly IArticleRepository _articleRepository;
    private readonly IFeedbackSentimentRepository _feedbackRepository;
    private readonly IMapper _mapper;
    
    public CreateArticleFeedbackCommandHandler(ITextAnalyticsService analyticsService,
        IArticleRepository articleRepository, IFeedbackSentimentRepository feedbackRepository, IMapper mapper)
    {
        _analyticsService = analyticsService;
        _articleRepository = articleRepository;
        _feedbackRepository = feedbackRepository;
        _mapper = mapper;
    }

    public async Task<Result<SentimentDto>> Handle(CreateArticleFeedbackCommand request,
        CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.ArticleId);

        if (article is null)
        {
            return Result<SentimentDto>.Failure(ArticleErrors.NotFound(request.ArticleId));
        }

        var existingFeedback =
            (await _feedbackRepository.FindAsync(f => f.ArticleId == article.Id && f.Type == FeedbackType.Article))
            .FirstOrDefault();

        if (existingFeedback is not null && article.Updated < existingFeedback.Created)
        {
            return _mapper.Map<SentimentDto>(existingFeedback);
        }

        var analysisResult = await _analyticsService.CreateSentimentAsync(article.Content);

        if (analysisResult.IsSuccess)
        {
            var sentimentEntity = _mapper.Map<FeedbackSentiment>(analysisResult.Value);
            
            sentimentEntity.SetArticleFeedback(article.Id);
            
            await _feedbackRepository.CreateAsync(sentimentEntity);
            
            return _mapper.Map<SentimentDto>(sentimentEntity);
        }
        
        return Result<SentimentDto>.Failure(analysisResult.Error);
    }
}