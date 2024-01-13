using FeedbackAnalyzer.Application.Features.Token;
using FeedbackAnalyzer.Application.Shared;
using Identity.Models;

namespace FeedbackAnalyzer.Application.Contracts.Services;

public interface IJwtTokenService
{
    Task<Result<TokenDto>> GenerateTokenPairAsync(ApplicationUser user);
    Task<Result<TokenDto>> RefreshTokenAsync(RefreshTokenDto tokenModel);
}