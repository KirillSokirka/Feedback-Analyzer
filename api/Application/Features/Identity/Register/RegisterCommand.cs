using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Identity.Register;

public record RegisterCommand(string Email, string FullName, string Password) : IRequest<Result<Unit>>;