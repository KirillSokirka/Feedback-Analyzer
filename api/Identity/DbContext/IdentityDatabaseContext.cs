using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.DbContext;

public class IdentityDatabaseContext : IdentityDbContext<ApplicationUser>
{
    public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options) : base(options)
    {
    }
}