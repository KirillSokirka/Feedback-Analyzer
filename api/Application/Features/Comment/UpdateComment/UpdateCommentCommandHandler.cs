using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared;
using FluentValidation;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Comment.UpdateComment;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Result<Unit>>
{
    private readonly IValidator<UpdateCommentCommand> _validator;
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public UpdateCommentCommandHandler(IValidator<UpdateCommentCommand> validator, ICommentRepository commentRepository,
        IMapper mapper)
    {
        _validator = validator;
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<Result<Unit>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            return Result<Unit>.Failure((validationResult.Errors.First().CustomState as Error)!);
        }
        
        var comment = await _commentRepository.GetByIdAsync(request.Id);
        
        _mapper.Map(request, comment);
        
        await _commentRepository.UpdateAsync(comment);

        return Unit.Value;
    }
}