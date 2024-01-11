﻿using FeedbackAnalyzer.Application.Abstraction;
using FeedbackAnalyzer.Application.Features.Token;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using FluentValidation;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FeedbackAnalyzer.Application.Features.Identity.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenDto>>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly AbstractValidator<LoginCommand> _validator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginCommandHandler(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, IJwtTokenService jwtTokenService,
        AbstractValidator<LoginCommand> validator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
        _validator = validator;
    }

    public async Task<Result<TokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            return Result<TokenDto>.Failure((validationResult.Errors.First().CustomState as Error)!);
        }

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

        if (result.IsSuccess)
        {
            identityUser.RefreshToken = result.Value.RefreshToken;
            identityUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(6);

            return result;
        }

        return Result<TokenDto>.Failure(result.Error);
    }
}