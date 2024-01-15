using FeedbackAnalyzer.Domain;
using Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbContext;

namespace Persistence;

public static class ApplicationDatabaseInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider, ApplicationUser? applicationUser)
    {
        var applicationContext = serviceProvider.GetRequiredService<ApplicationDatabaseContext>();

        if ((await applicationContext.Database.GetPendingMigrationsAsync()).Any())
        {
            await applicationContext.Database.MigrateAsync();
        }

        await SeedTestUser(applicationUser, applicationContext);
    }

    private static async Task SeedTestUser(ApplicationUser? applicationUser,
        ApplicationDatabaseContext applicationContext)
    {
        if (applicationUser is not null)
        {
            if (applicationContext.Users.FirstOrDefault(u => applicationUser.Id == u.IdentityId) is null)
            {
                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    IdentityId = applicationUser.Id,
                    FullName = applicationUser.FullName
                };

                applicationContext.Users.Add(user);
                await applicationContext.SaveChangesAsync();
            }
        }
    }
}