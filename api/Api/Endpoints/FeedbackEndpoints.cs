using FeedbackAnalyzer.Api.Extensions;
using FeedbackAnalyzer.Application.Features.Article.CreateArticleFeedback;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAnalyzer.Api.Endpoints;

public static class FeedbackEndpoints
{
    public static void AddFeedbackEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/articles/{articleId}/feedback", async ([FromRoute] string articleId,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateArticleFeedbackCommand(articleId));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : result.ToProblemDetails();
        });
    }
}