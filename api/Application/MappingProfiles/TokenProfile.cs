using AutoMapper;
using FeedbackAnalyzer.Application.Features.Token;

namespace FeedbackAnalyzer.Application.MappingProfiles;

public class TokenProfile : Profile
{
    public TokenProfile()
    {
        CreateMap<TokenRefreshCommand, RefreshTokenDto>();
    }
}