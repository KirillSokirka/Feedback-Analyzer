using FeedbackAnalyzer.Application.Contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}