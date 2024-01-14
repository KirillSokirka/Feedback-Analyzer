using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAnalyzer.Api.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _delegate;

    public ExceptionHandlerMiddleware(RequestDelegate delegateNext)
    {
        _delegate = delegateNext;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _delegate(context);
        }
        catch (Exception exception)
        {
            var problemDetail = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "Server Error"
            };

            context.Response.StatusCode = problemDetail.Status.Value;
            context.Response.ContentType = "application/problem+json"; 

            var problemDetailJson = JsonSerializer.Serialize(problemDetail); 
            await context.Response.WriteAsync(problemDetailJson);
        }
    }
}