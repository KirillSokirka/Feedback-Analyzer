namespace FeedbackAnalyzer.Application.Contracts.DTOs;

public class UserDetailDto : UserDto
{
    public List<ArticleDto>? Articles { get; set; }
}