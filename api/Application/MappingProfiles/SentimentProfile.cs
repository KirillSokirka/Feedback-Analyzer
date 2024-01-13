using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Domain;

namespace FeedbackAnalyzer.Application.MappingProfiles;

public class SentimentProfile : Profile
{
    public SentimentProfile()
    {
        CreateMap<FeedbackSentiment, SentimentDto>();
        
        CreateMap<SentimentDto, FeedbackSentiment>()
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now));
    }
}