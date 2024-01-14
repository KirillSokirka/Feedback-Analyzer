using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Feedback.CreateUserArticlesFeedback;

public record CreateUserArticlesFeedbackCommand(string UserId) : IRequest<Result<SentimentDto?>>;