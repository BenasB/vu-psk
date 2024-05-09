namespace Recipes.Public;

public class Recipe
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required IEnumerable<Ingredient> Ingredients { get; init; }
    public required IEnumerable<string> Instructions { get; init; }
}