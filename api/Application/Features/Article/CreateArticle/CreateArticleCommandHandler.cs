using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared;
using FluentValidation;
using MediatR;
using Microsoft.Identity.Client;

namespace FeedbackAnalyzer.Application.Features.Article.CreateArticle;

public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Result<string>>
{
    private readonly IValidator<CreateArticleCommand> _validator;
    private readonly IArticleRepository _articleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateArticleCommandHandler(IArticleRepository articleRepository, IMapper mapper,
        IValidator<CreateArticleCommand> validator, IUserRepository userRepository)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
        _validator = validator;
        _userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            return Result<string>.Failure((validationResult.Errors.First().CustomState as Error)!);
        }

        var creator = (await _userRepository.FindAsync(u => u.IdentityId == request.CreatorId)).First();
        
        var articleToCreate = _mapper.Map<Domain.Article>(request);

        articleToCreate.CreatorId = creator.Id;
        
        await _articleRepository.CreateAsync(articleToCreate);

        return articleToCreate.Id;
    }
}