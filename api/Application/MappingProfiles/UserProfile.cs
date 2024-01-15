using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Features.Identity.Register;
using FeedbackAnalyzer.Application.Features.User;
using FeedbackAnalyzer.Domain;
using Identity.Models;

namespace FeedbackAnalyzer.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, User>()
            .ForMember(dest => dest.IdentityId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid().ToString()));

        CreateMap<RegisterCommand, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FullName.Replace(" ", "")));

        CreateMap<User, UserDto>();

        CreateMap<User, UserDetailDto>();
    }
}