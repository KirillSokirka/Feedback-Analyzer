using FeedbackAnalyzer.Api.Extensions;
using FeedbackAnalyzer.Application.Features.Identity.Register;
using MediatR;

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
    }
}