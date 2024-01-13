using FeedbackAnalyzer.Domain;

namespace FeedbackAnalyzer.Application.Contracts.Persistence;

public interface IFeedbackSentimentRepository : IGenericRepository<FeedbackSentiment>
{
}