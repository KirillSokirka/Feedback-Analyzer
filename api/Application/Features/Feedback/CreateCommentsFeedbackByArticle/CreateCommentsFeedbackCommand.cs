using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Feedback.CreateCommentsFeedbackByArticle;

public record CreateCommentsFeedbackCommand(string ArticleId) : IRequest<Result<SentimentDto>>;