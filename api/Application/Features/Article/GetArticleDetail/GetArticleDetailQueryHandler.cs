using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.GetArticleDetail;

public class GetArticleDetailQueryHandler : IRequestHandler<GetArticleDetailQuery, Result<ArticleDetailDto>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticleDetailQueryHandler(IMapper mapper, IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<Result<ArticleDetailDto>> Handle(GetArticleDetailQuery request,
        CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.Id);

        return article is null
            ? Result<ArticleDetailDto>.Failure(ArticleErrors.NotFound(request.Id))
            : _mapper.Map<ArticleDetailDto>(article);
    }
}