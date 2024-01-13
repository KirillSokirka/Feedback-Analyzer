using FeedbackAnalyzer.Application.Contracts.DTOs;

namespace FeedbackAnalyzer.Application.Contracts.Services;

public interface ITextAnalyticsService
{
    Task<SentimentDto> CreateAverageSentiment(IEnumerable<string> text);
    
    Task<SentimentDto> CreateSentiment(string text);
}