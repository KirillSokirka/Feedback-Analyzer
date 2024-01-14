using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Features.Article.CreateArticle;
using FeedbackAnalyzer.Application.Features.Article.GetAllArticles;
using FeedbackAnalyzer.Application.Features.Article.GetArticleDetail;
using FeedbackAnalyzer.Application.Features.Article.UpdateArticle;
using FeedbackAnalyzer.Domain;

namespace FeedbackAnalyzer.Application.MappingProfiles;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleDto>()
            .ForMember(x => x.Creator, expression => expression.MapFrom(x => x.Creator.FullName));

        CreateMap<Article, ArticleDetailDto>();

        CreateMap<CreateArticleCommand, Article>()
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<UpdateArticleCommand, Article>()
            .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != null))
            .ForMember(dest => dest.Content, opt => opt.Condition(src => src.Content != null))
            .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.Creator, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore());
    }
}