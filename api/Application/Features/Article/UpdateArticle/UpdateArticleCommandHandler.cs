using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.UpdateArticle;

public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Result<string>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public UpdateArticleCommandHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<string>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.Id);

        if (article is null)
        {
            return Result<string>.Failure(ArticleErrors.NotFound(request.Id));
        }

        _mapper.Map(request, article);

        await _articleRepository.UpdateAsync(article);

        return article.Id;
    }
}