using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Features.Comment;
using FeedbackAnalyzer.Application.Features.Comment.CreateComment;
using FeedbackAnalyzer.Application.Features.Comment.UpdateComment;
using FeedbackAnalyzer.Domain;

namespace FeedbackAnalyzer.Application.MappingProfiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CreateCommentCommand, Comment>()
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<UpdateCommentCommand, Comment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ArticleId, opt => opt.Ignore());
        
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.Commenator, opt => opt.MapFrom(src => src.Commentator.FullName));
    }
}