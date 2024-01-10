namespace FeedbackAnalyzer.Application.Shared.EntityErrors;

public static class ArticleErrors
{
    public static Error NotFound(string id) => 
        Error.NotFound("Article.NotFound", $"The article with Id = {id} was not found");
}