using System.Linq.Expressions;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContext;

namespace Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ApplicationDatabaseContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public GenericRepository(ApplicationDatabaseContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }
    
    public virtual async Task<List<TEntity>> GetAsync()
        => await DbSet.ToListAsync();
    
    public virtual async Task<TEntity?> GetByIdAsync(string id)
        => await DbSet.FindAsync(id);
    
    public virtual async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        => await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    
    public virtual async Task CreateAsync(TEntity entity)
    {
        await Context.AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async  Task UpdateAsync(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        Context.Remove(entity);
        await Context.SaveChangesAsync();
    }
}