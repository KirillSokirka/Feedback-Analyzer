using FeedbackAnalyzer.Application.Contracts.Services;
using Infrastructure.Authentication;
using Infrastructure.TextAnalytics;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<ITextAnalyticsService, TextAnalyticsService>();
        
        return services;
    }
}