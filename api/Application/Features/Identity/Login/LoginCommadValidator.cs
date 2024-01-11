using FeedbackAnalyzer.Application.Shared;
using FluentValidation;

namespace FeedbackAnalyzer.Application.Features.Identity.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .WithState(_ => Error.Validation("User.Email.InvalidFormat", "The email should not be empty"))
            .EmailAddress()
            .WithState(_ => Error.Validation("User.Email.InvalidFormat", "The email format is invalid."));

        RuleFor(p => p.Password)
            .NotEmpty()
            .WithState(_ => Error.Validation("User.Password.Empty", "The password should not be empty"));
    }
}