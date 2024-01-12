using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Domain;
using Persistence.DbContext;

namespace Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDatabaseContext context) 
        : base(context)
    {
    }
}