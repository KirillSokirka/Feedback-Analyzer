using FeedbackAnalyzer.Application.Contracts.DTOs;
using FeedbackAnalyzer.Application.Contracts.Persistence;
using FeedbackAnalyzer.Application.Contracts.Services;
using FeedbackAnalyzer.Application.Shared;
using FeedbackAnalyzer.Application.Shared.EntityErrors;
using MediatR;

namespace FeedbackAnalyzer.Application.Features.Feedback.CreateUserArticlesFeedback;

public class CreateUserArticleFeedbackCommandHandler
    : IRequestHandler<CreateUserArticlesFeedbackCommand, Result<SentimentDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IFeedbackService _feedbackService;

    public CreateUserArticleFeedbackCommandHandler(IUserRepository userRepository, IFeedbackService feedbackService)
    {
        _userRepository = userRepository;
        _feedbackService = feedbackService;
    }

    public async Task<Result<SentimentDto>> Handle(CreateUserArticlesFeedbackCommand request,
        CancellationToken cancellationToken)
    {
        var user = (await _userRepository.FindAsync(u => u.IdentityId == request.UserId)).FirstOrDefault();

        if (user is null)
        {
            return Result<SentimentDto>.Failure(UserErrors.NotFoundById(request.UserId));
        }

        var feedback = await _feedbackService.GetUserArticlesFeedbackAsync(user);

        return feedback.IsSuccess ? feedback : Result<SentimentDto>.Failure(feedback.Error);
    }
}