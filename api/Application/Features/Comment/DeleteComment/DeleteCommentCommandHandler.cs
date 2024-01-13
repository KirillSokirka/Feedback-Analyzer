using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared;
using FluentValidation;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Comment.DeleteComment;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result<Unit>>
{
    private readonly IValidator<DeleteCommentCommand> _validator;
    private readonly ICommentRepository _commentRepository;
    
    public DeleteCommentCommandHandler(ICommentRepository commentRepository,
        IValidator<DeleteCommentCommand> validator)
    {
        _commentRepository = commentRepository;
        _validator = validator;
    }
    
    public async Task<Result<Unit>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            return Result<Unit>.Failure((validationResult.Errors.First().CustomState as Error)!);
        }
        
        var article = await _commentRepository.GetByIdAsync(request.Id);
        
        await _commentRepository.DeleteAsync(article);

        return Unit.Value;
    }
}