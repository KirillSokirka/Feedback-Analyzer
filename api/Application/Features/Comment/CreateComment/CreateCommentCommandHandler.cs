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
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateCommentCommandHandler(IValidator<CreateCommentCommand> validator, 
        ICommentRepository commentRepository, IMapper mapper, IUserRepository userRepository)
    {
        _validator = validator;
        _commentRepository = commentRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            return Result<string>.Failure((validationResult.Errors.First().CustomState as Error)!);
        }
        
        var creator = (await _userRepository.FindAsync(u => u.IdentityId == request.CommentatorId)).First();
        
        var comment = _mapper.Map<Domain.Comment>(request);
        
        comment.CommentatorId = creator.Id;
        
        await _commentRepository.CreateAsync(comment);

        return comment.Id;
    }
}