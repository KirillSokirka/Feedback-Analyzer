using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Domain;

namespace FeedbackAnalyzer.Application.Contracts.Services;

public interface IFeedbackService
{
    Task<Result<SentimentDto>> GetArticleCommentsFeedbackAsync(Article article);
    Task<Result<SentimentDto>> GetArticleFeedbackAsync(Article article);
    Task<Result<SentimentDto>> GetUserArticlesFeedbackAsync(User article);
}