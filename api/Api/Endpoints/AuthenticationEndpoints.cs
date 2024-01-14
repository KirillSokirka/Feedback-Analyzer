using FeedbackAnalyzer.Api.Extensions;
using FeedbackAnalyzer.Application.Features.Identity.Login;
using FeedbackAnalyzer.Application.Features.Identity.Register;
using FeedbackAnalyzer.Application.Features.Token;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace FeedbackAnalyzer.Api.Endpoints;

public static class AuthenticationEndpoints
{
    public static void AddAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/register", async (RegisterCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
        });
        
        app.MapPost("/login", async (LoginCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        });
        
        app.MapPost("/refresh-token", [Authorize]
            async (TokenRefreshCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        });
    }
}