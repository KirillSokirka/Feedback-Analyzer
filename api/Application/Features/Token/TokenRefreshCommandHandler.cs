using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.Services;
using FeedbackAnalyzer.Application.Shared;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FeedbackAnalyzer.Application.Features.Token;

public class TokenRefreshCommandHandler : IRequestHandler<TokenRefreshCommand, Result<TokenDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IMapper _mapper;
    
    public TokenRefreshCommandHandler(IJwtTokenService jwtTokenService, 
        UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _jwtTokenService = jwtTokenService;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<TokenDto>> Handle(TokenRefreshCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenDto = _mapper.Map<RefreshTokenDto>(request);

        var result = await _jwtTokenService.RefreshTokenAsync(refreshTokenDto);

        if (result.IsFailure)
        {
            return Result<TokenDto>.Failure(result.Error);
        } 
        
        var identityUser = (await _userManager.FindByEmailAsync(request.Email))!;
            
        identityUser.RefreshToken = result.Value.RefreshToken;
        identityUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(6);

        var identityResult = await _userManager.UpdateAsync(identityUser);

        if (identityResult.Succeeded)
        {
            return result.Value;
        }

        var error = identityResult.Errors.First();

        return Result<TokenDto>.Failure(Error.Failure(error.Code, error.Description));

    }
}