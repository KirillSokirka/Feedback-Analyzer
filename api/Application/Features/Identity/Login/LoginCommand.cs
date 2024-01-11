using FeedbackAnalyzer.Application.Features.Token;
using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Identity.Login;

public record LoginCommand(string Email, string Password) : IRequest<Result<TokenDto>>;