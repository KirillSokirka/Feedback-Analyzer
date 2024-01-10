using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.CreateArticle;

public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Result<string>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public CreateArticleCommandHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var articleToCreate = _mapper.Map<Domain.Article>(request);

        await _articleRepository.CreateAsync(articleToCreate);

        return articleToCreate.Id;
    }
}