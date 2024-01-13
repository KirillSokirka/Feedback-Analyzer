namespace FeedbackAnalyzer.Application.Shared.EntityErrors;

public static class CommentErrors
{
    public static Error NotFound(string id) =>
        Error.NotFound("Comment.NotFound", $"The Comment with Id = {id} was not found");

    public static Error NotValidComment() =>
        Error.Validation("Comment.NotValid", "The Text should be not empty and less than 100 symbols");

    public static Error NotFoundArticle(string id) =>
        Error.NotFound("Comment.Article.NotFound", $"The Article with Id = {id} was not found");

    public static Error NotFoundCommentator(string id) =>
        Error.NotFound("Comment.Commentator.NotFound", $"The User with Id = {id} was not found");

    public static Error CommentDoesNotBelongToArticle(string commentId, string articleId) =>
        Error.Conflict("Comment.Conflict",
            $"The Comment with Id = {commentId} doesn't belong Article with Id = {articleId}");
}