using System.ComponentModel.DataAnnotations;

namespace Recipes.Public;

public class Recipe
{
    public int Id { get; set; }
    public required string Title { get; init; }
    public int AuthorId { get; set; }
    public string? Description { get; set; }
    public TimeSpan CookingTime { get; set; }
    public int Servings { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required IEnumerable<string> Ingredients { get; init; }
    public required IEnumerable<string> Instructions { get; init; }
}