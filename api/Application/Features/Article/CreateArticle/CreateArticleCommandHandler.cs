using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared;
using FluentValidation;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.CreateArticle;

public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Result<string>>
{
    private readonly IValidator<CreateArticleCommand> _validator;
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public CreateArticleCommandHandler(IArticleRepository articleRepository, IMapper mapper,
        IValidator<CreateArticleCommand> validator)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<string>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            return Result<string>.Failure((validationResult.Errors.First().CustomState as Error)!);
        }

        var articleToCreate = _mapper.Map<Domain.Article>(request);

        await _articleRepository.CreateAsync(articleToCreate);

        return articleToCreate.Id;
    }
}