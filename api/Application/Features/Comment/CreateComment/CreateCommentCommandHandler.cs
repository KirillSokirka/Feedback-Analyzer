using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared;
using FluentValidation;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Comment.CreateComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Result<string>>
{
    private readonly IValidator<CreateCommentCommand> _validator;
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CreateCommentCommandHandler(IValidator<CreateCommentCommand> validator, 
        ICommentRepository commentRepository, IMapper mapper)
    {
        _validator = validator;
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            return Result<string>.Failure((validationResult.Errors.First().CustomState as Error)!);
        }

        var comment = _mapper.Map<Domain.Comment>(request);

        await _commentRepository.CreateAsync(comment);

        return comment.Id;
    }
}