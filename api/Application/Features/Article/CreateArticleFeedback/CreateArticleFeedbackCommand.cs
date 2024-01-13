using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.CreateArticleFeedback;

public record CreateArticleFeedbackCommand(string ArticleId) : IRequest<Result<SentimentDto>>;