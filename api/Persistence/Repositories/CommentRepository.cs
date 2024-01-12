using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Domain;
using Persistence.DbContext;

namespace Persistence.Repositories;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(ApplicationDatabaseContext context) : base(context)
    {
    }
}