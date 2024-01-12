using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Token;

public record TokenRefreshCommand(string Email, string AccessToken, string RefreshToken) : IRequest<Result<TokenDto>>;
