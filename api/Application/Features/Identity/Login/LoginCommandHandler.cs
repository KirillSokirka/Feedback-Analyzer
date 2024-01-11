using FeedbackAnalyzer.Application.Abstraction;
using FeedbackAnalyzer.Application.Features.Token;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FeedbackAnalyzer.Application.Features.Identity.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginCommandHandler(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Result<TokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var identityUser = await _userManager.FindByEmailAsync(request.Email);

        if (identityUser is null)
        {
            return Result<TokenDto>.Failure(IdentityUserErrors.NotFound(request.Email));
        }

        var loginResult = await _signInManager.CheckPasswordSignInAsync(identityUser, request.Password, false);

        if (loginResult.Succeeded == false)
        {
            return Result<TokenDto>.Failure(IdentityUserErrors.NotValidCredential(request.Email));
        }

        var result = await _jwtTokenService.GenerateTokenPairAsync(identityUser);

        return result.IsSuccess ? result : Result<TokenDto>.Failure(result.Error);
    }
}