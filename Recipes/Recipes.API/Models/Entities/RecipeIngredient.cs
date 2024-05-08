using System.ComponentModel.DataAnnotations;

namespace Recipes.API.Models.Entities;
public class RecipeIngredient
{
    [Required]
    public int RecipeId { get; set; }

    public RecipeEntity Recipe { get; set; }

    public int IngredientId { get; set; }

    public IngredientEntity Ingredient { get; set; }

    public string Quantity { get; set; }
}
