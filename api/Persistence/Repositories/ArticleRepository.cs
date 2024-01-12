using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Domain;
using Persistence.DbContext;

namespace Persistence.Repositories;

public class ArticleRepository : GenericRepository<Article>, IArticleRepository
{
    public ArticleRepository(ApplicationDatabaseContext context) : base(context)
    { }
}