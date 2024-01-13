using FeedbackAnalyzer.Api.Extensions;
using FeedbackAnalyzer.Application.Features.Article.CreateArticle;
using FeedbackAnalyzer.Application.Features.Article.DeleteArticle;
using FeedbackAnalyzer.Application.Features.Article.GetAllArticles;
using FeedbackAnalyzer.Application.Features.Article.GetArticleDetail;
using FeedbackAnalyzer.Application.Features.Article.UpdateArticle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAnalyzer.Api.Endpoints;

public static class ArticleEndpoints
{
    public static void AddArticleEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/articles", async ([FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetArticlesQuery());

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        });

        app.MapGet("/articles/{id}", async ([FromRoute] string id, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetArticleDetailQuery(id));

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        });

        app.MapPost("/articles", async ([FromBody] CreateArticleCommand command, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(command);

            return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
        });

        app.MapPut("/articles", async ([FromBody] UpdateArticleCommand command, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(command);

            return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
        });
        
        app.MapDelete("/articles/{id}", async ([FromRoute] string id, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new DeleteArticleCommand(id));

            return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
        });
    }
}