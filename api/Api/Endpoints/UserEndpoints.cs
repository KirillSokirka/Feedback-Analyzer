using FeedbackAnalyzer.Api.Extensions;
using FeedbackAnalyzer.Application.Features.User.GetUserDetail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAnalyzer.Api.Endpoints;

public static class UserEndpoints
{
    public static void AddUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users/info", [Authorize]
            async ([FromServices] ISender sender, HttpContext context) =>
            {
                var userId = context.User.FindFirst("uid")?.Value;

                var result = await sender.Send(new GetUserDetailQuery(userId));

                return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
            });
    }
}