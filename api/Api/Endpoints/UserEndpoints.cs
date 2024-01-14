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
        app.MapGet("/users/{id}", [Authorize] async ([FromRoute] string id, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetUserDetailQuery(id));

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        });
    }
}