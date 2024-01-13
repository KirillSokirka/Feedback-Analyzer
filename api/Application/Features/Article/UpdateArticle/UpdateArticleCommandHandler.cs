using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using FluentValidation;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Article.UpdateArticle;

public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Result<string>>
{
    private readonly IValidator<UpdateArticleCommand> _validator;
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    
    public UpdateArticleCommandHandler(IArticleRepository articleRepository, IMapper mapper,
        IValidator<UpdateArticleCommand> validator)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<string>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            return Result<string>.Failure((validationResult.Errors.First().CustomState as Error)!);
        }
        
        var article = await _articleRepository.GetByIdAsync(request.Id);
        
        _mapper.Map(request, article);

        await _articleRepository.UpdateAsync(article);

        return article.Id;
    }
}