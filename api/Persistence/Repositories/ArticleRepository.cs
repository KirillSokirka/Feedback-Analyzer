using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContext;

namespace Persistence.Repositories;

public class ArticleRepository : GenericRepository<Article>, IArticleRepository
{
    public ArticleRepository(ApplicationDatabaseContext context) : base(context)
    {
    }

    public override async Task<List<Article>> GetAsync()
        => await DbSet
            .AsNoTracking()
            .Include(a => a.Creator)
            .ToListAsync();

    public override async Task<Article?> GetByIdAsync(string id)
        => await DbSet
            .AsNoTracking()
            .Include(a => a.Creator)
            .Include(a => a.Comments).ThenInclude(a => a.Commentator).AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
}