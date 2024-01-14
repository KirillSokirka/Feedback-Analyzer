using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.GetAllArticles;

public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, Result<List<ArticleDto>>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticlesQueryHandler(IMapper mapper, IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ArticleDto>>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
    {
        var articles = await _articleRepository.GetAsync();

        return _mapper.Map<List<ArticleDto>>(articles);
    }
}