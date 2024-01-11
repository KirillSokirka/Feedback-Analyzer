using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using FeedbackAnalyzer.Domain;
using FluentValidation;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FeedbackAnalyzer.Application.Features.Identity.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterCommandValidator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;

        RuleFor(p => p.FullName)
            .NotEmpty()
            .WithMessage("Fullname should not be null/empty");

        RuleFor(p => p.Email)
            .NotEmpty()
            .WithState(_ => Error.Validation("User.Email.InvalidFormat", "The email should not be empty"))
            .EmailAddress()
            .WithState(_ => Error.Validation("User.Email.InvalidFormat", "The email format is invalid."))
            .MustAsync(EmailShouldBeUnique)
            .WithState(_ => IdentityUserErrors.Conflict());

        RuleFor(p => p.Password)
            .NotEmpty()
            .WithState(_ => Error.Validation("User.Password.Empty", "The password should not be empty"))
            .MinimumLength(6)
            .WithState(_ => Error.Validation("User.Password.Length", "The password must be at least 6 characters long"))
            .Matches(@"(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]")
            .WithMessage("Password must contain letters and numbers")
            .WithState(_ => Error.Validation("User.Password.Format", "The password must contain letters and numbers"));
    }

    private async Task<bool> EmailShouldBeUnique(string email, CancellationToken ct)
        => await _userManager.Users.FirstOrDefaultAsync(user => user.Email == email, ct) is null;
}