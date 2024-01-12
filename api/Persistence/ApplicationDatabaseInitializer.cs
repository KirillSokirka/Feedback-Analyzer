using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbContext;

namespace Persistence;

public static class ApplicationDatabaseInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var identityContext = serviceProvider.GetRequiredService<ApplicationDatabaseContext>();

        if ((await identityContext.Database.GetPendingMigrationsAsync()).Any())
        {
            await identityContext.Database.MigrateAsync();
        }
    }
}