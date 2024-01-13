using FeedbackAnalyzer.Application.Features.Article.GetAllArticles;
using FeedbackAnalyzer.Application.Features.Comment;

namespace FeedbackAnalyzer.Application.Features.User.GetUserDetail;

public class UserDetailDto : UserDto
{
    public List<ArticleDto>? Articles { get; set; }
    public List<CommentDto>? LeavedComments { get; set; }
}