using FeedbackAnalyzer.Api.Extensions;
using FeedbackAnalyzer.Application.Features.Comment.CreateCommentsFeedbackByArticle;
using FeedbackAnalyzer.Application.Features.Feedback;
using FeedbackAnalyzer.Application.Features.Feedback.CreateArticleFeedback;
using FeedbackAnalyzer.Application.Features.Feedback.CreateUserArticlesFeedback;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAnalyzer.Api.Endpoints;

public static class FeedbackEndpoints
{
    public static void AddFeedbackEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/articles/{articleId}/feedback", [Authorize] async ([FromRoute] string articleId,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateArticleFeedbackCommand(articleId));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : result.ToProblemDetails();
        });

        app.MapPost("/articles/{articleId}/comments/feedback", [Authorize] async ([FromRoute] string articleId,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateCommentsFeedbackCommand(articleId));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : result.ToProblemDetails();
        });

        app.MapPost("/users/{userId}/articles/feedback", [Authorize] async ([FromRoute] string userId,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateUserArticlesFeedbackCommand(userId));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : result.ToProblemDetails();
        });
    }
}