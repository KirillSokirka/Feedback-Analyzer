using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Domain;
using Persistence.DbContext;

namespace Persistence.Repositories;

public class FeedbackSentimentRepository : GenericRepository<FeedbackSentiment>, IFeedbackSentimentRepository
{
    public FeedbackSentimentRepository(ApplicationDatabaseContext context) : base(context)
    {
    }
}