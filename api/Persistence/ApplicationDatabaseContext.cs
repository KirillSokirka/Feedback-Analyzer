using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ApplicationDatabaseContext: DbContext
{
    public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options)
    {
    }
}