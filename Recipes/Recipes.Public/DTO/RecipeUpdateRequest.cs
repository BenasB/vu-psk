namespace Recipes.Public.DTO;

public class RecipeUpdateRequest
{
    public int AuthorId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public TimeSpan CookingTime { get; set; }
    public int Servings { get; set; }
    public required IList<string> Instructions { get; set; } = new List<string>();
    public required IList<string> Ingredients { get; set; } = new List<string>();
}
