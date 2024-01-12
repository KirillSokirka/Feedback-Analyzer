using Identity.DbContext;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity;

public static class IdentityDatabaseInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var identityContext = serviceProvider.GetRequiredService<IdentityDatabaseContext>();

        if ((await identityContext.Database.GetPendingMigrationsAsync()).Any())
        {
            await identityContext.Database.MigrateAsync();
        }
        
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await SeedRolesAsync(roleManager);
        await SeedTestUserAsync(userManager);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Administrator", "User" };
        
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }

    private static async Task SeedTestUserAsync(UserManager<ApplicationUser> userManager)
    {
        const string email = "admin@google.com";
        const string userName = "administrator";
        const string password = "password1";

        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                FullName = userName,
                Email = email
            };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                string[] roleNames = { "Administrator", "User" };

                foreach (var roleName in roleNames)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}