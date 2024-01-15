using FeedbackAnalyzer.Api.Extensions;
using FeedbackAnalyzer.Application.Features.Feedback;
using FeedbackAnalyzer.Application.Features.Feedback.CreateArticleFeedback;
using FeedbackAnalyzer.Application.Features.Feedback.CreateCommentsFeedbackByArticle;
using FeedbackAnalyzer.Application.Features.Feedback.CreateUserArticlesFeedback;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAnalyzer.Api.Endpoints;

public static class FeedbackEndpoints
{
    public static void AddFeedbackEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/articles/{articleId}/feedback", async ([FromRoute] string articleId,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateArticleFeedbackCommand(articleId));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : result.ToProblemDetails();
        });

        app.MapGet("/articles/{articleId}/comments/feedback", async ([FromRoute] string articleId,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateCommentsFeedbackCommand(articleId));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : result.ToProblemDetails();
        });

        app.MapGet("/users/{userId}/articles/feedback", [Authorize] async ([FromRoute] string userId,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateUserArticlesFeedbackCommand(userId));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : result.ToProblemDetails();
        });
    }
}