using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Comment.CreateCommentsFeedbackByArticle;

public record CreateCommentsFeedbackCommand(string ArticleId) : IRequest<Result<SentimentDto?>>;