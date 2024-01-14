using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.User.GetUserDetail;

public record GetUserDetailQuery(string Id) : IRequest<Result<UserDetailDto>>;