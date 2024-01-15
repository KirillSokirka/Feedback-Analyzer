using System.Linq.Expressions;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContext;

namespace Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDatabaseContext context)
        : base(context)
    {
    }

    public override async Task<User?> GetByIdAsync(string id)
        => await DbSet
            .AsNoTracking()
            .Include(a => a.Articles)
            .FirstOrDefaultAsync(p => p.Id == id);

    public override Task<List<User>> FindAsync(Expression<Func<User, bool>> predicate)
        => DbSet
            .AsNoTracking()
            .Include(a => a.Articles)
            .Where(predicate)
            .ToListAsync();
}