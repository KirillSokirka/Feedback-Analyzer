using FeedbackAnalyzer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Persistence.DbContext;

public class ApplicationDatabaseContext: Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options)
    {
    }
    
    public DbSet<Article> Articles { get; set; } 
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigurePrimaryKeys(modelBuilder);
        ConfigureRelationships(modelBuilder);
    }
    
    private static void ConfigurePrimaryKeys(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>().HasKey(e => e.Id);
        modelBuilder.Entity<Comment>().HasKey(e => e.Id);
        modelBuilder.Entity<User>().HasKey(e => e.Id);
    }

    private static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>()
            .HasOne(a => a.Creator)
            .WithMany(u => u.Articles)
            .HasForeignKey(a => a.CreatorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Article)
            .WithMany(a => a.Comments)
            .HasForeignKey(c => c.ArticleId)
            .OnDelete(DeleteBehavior.Cascade); 
        
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Commentator)
            .WithMany(u => u.LeavedComments)
            .HasForeignKey(c => c.CommentatorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}