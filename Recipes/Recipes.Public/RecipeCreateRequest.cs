namespace Recipes.Public;

public class RecipeCreateRequest
{
    public int AuthorId { get; set; }   
    public required string Title { get; set; }
    public string? Description { get; set; }
    public TimeSpan CookingTime { get; set; }
    public int Servings { get; set; }
    public required IList<string> Instructions { get; set; } = new List<string>();
    public required IList<string> Ingredients { get; set; } = new List<string>();
    public IList<string> Tags { get; set; } = new List<string>();
}
