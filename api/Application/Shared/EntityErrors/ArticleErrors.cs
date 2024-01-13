namespace FeedbackAnalyzer.Application.Shared.EntityErrors;

public static class ArticleErrors
{
    public static Error NotFound(string id) => 
        Error.NotFound("Article.NotFound", $"The article with Id = {id} was not found");
    
    public static Error NotValidTitle() => 
        Error.Validation("Article.Validation", "The article title should be not empty and less then 50 symbols");
    
    public static Error NotValidContent() => 
        Error.Validation("Article.Validation", "The article content should not be empty");
    
    public static Error ArticleCreatorNotFound(string id) => 
        Error.NotFound("Article.Validation", $"The article creator with Id = {id} was not found");
}