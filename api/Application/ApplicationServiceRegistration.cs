using System.Reflection;
using FeedbackAnalyzer.Application.Contracts.Services;
using FeedbackAnalyzer.Application.Features.Identity.Login;
using FeedbackAnalyzer.Application.Features.Identity.Register;
using FeedbackAnalyzer.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FeedbackAnalyzer.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IFeedbackService, FeedbackService>();
        
        return services;
    }
}