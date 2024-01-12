using System.Reflection;
using FeedbackAnalyzer.Application.Features.Identity.Login;
using FeedbackAnalyzer.Application.Features.Identity.Register;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FeedbackAnalyzer.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        services.AddTransient<AbstractValidator<LoginCommand>, LoginCommandValidator>();
        services.AddTransient<AbstractValidator<RegisterCommand>, RegisterCommandValidator>();

        return services;
    }
}