namespace FeedbackAnalyzer.Application.Features.Token;

public record TokenDto(string AccessToken, string RefreshToken);

public record RefreshTokenDto(string Email, string AccessToken, string RefreshToken);

