using AutoMapper;
using FeedbackAnalyzer.Application.Features.Identity.Register;
using FeedbackAnalyzer.Application.Features.User;
using FeedbackAnalyzer.Domain;
using Identity.Models;

namespace FeedbackAnalyzer.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, User>();
        
        CreateMap<RegisterCommand, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FullName.Replace(" ", "")));

        CreateMap<User, UserDto>();

    }
}