using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Shared;

namespace FeedbackAnalyzer.Application.Contracts.Services;

public interface ITextAnalyticsService
{
    Task<Result<SentimentDto>> CreateAverageSentimentAsync(IEnumerable<string> text);
    Task<Result<SentimentDto>> CreateSentimentAsync(string text);
}