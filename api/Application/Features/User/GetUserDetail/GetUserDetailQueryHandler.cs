using AutoMapper;
using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.User.GetUserDetail;

public record GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, Result<UserDetailDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserDetailQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserDetailDto>> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        if (request.Id is null)
        {
            return Result<UserDetailDto>.Failure(UserErrors.NotValidToken()); 
        }
        
        var user = (await _userRepository.FindAsync(u => u.IdentityId == request.Id)).FirstOrDefault();

        return user is not null
            ? _mapper.Map<UserDetailDto>(user)
            : Result<UserDetailDto>.Failure(UserErrors.NotFoundById(request.Id));
    }
}