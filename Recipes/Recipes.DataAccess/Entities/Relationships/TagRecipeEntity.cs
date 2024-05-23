namespace Recipes.DataAccess.Entities.Relationships;

public class TagRecipeEntity
{
    public int RecipeId { get; set; }
    public RecipeEntity Recipe { get; set; } = null!;

    public int TagId { get; set; }
    public TagEntity Tag { get; set; } = null!;
}
