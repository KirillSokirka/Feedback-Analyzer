using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared;
using FluentValidation;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FeedbackAnalyzer.Application.Features.Identity.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<Unit>>
{
    private readonly IValidator<RegisterCommand> _validator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IUserRepository userRepository, UserManager<ApplicationUser> userManager,
        IValidator<RegisterCommand> validator, IMapper mapper)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Result<Unit>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            return Result<Unit>.Failure((validationResult.Errors.First().CustomState as Error)!);
        }

        var identityUser = _mapper.Map<ApplicationUser>(request);

        var creationResult = await _userManager.CreateAsync(identityUser, request.Password);

        if (creationResult.Succeeded)
        {
            var user = _mapper.Map<Domain.User>(identityUser);

            await _userRepository.CreateAsync(user);
            
            return Unit.Value;
        }

        var error = creationResult.Errors.First();

        return Result<Unit>.Failure(Error.Failure(error.Code, error.Description));
    }
}