using FeedbackAnalyzer.Api.Extensions;
using FeedbackAnalyzer.Application.Features.Comment.CreateComment;
using FeedbackAnalyzer.Application.Features.Comment.DeleteComment;
using FeedbackAnalyzer.Application.Features.Comment.UpdateComment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAnalyzer.Api.Endpoints;

public static class CommentEndpoints
{
    public static void AddCommentEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/articles/{articleId}/comments", [Authorize] async ([FromRoute] string articleId,
            [FromBody] CreateCommentDto commentDto,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateCommentCommand(
                commentDto.Text,
                commentDto.CommentatorId,
                articleId));

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : result.ToProblemDetails();
        });

        app.MapPut("/articles/{articleId}/comments/{commentId}", [Authorize] async ([FromRoute] string articleId,
            [FromRoute] string commentId,
            [FromBody] string text,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new UpdateCommentCommand(commentId, text, articleId));

            return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
        });

        app.MapDelete("/articles/{articleId}/comments/{commentId}", [Authorize] async ([FromRoute] string articleId,
            [FromRoute] string commentId, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new DeleteCommentCommand(commentId, articleId));

            return result.IsSuccess ? Results.NoContent() : result.ToProblemDetails();
        });
    }
}