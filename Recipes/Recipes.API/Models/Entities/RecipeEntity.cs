using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Recipes.API.Models.Entities;

public class RecipeEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int AuthorId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public TimeSpan CookingTime { get; set; }

    public int Servings { get; set; }

    public DateTime UpdatedAt { get; set; }

    public IList<string> Instructions { get; set; } = new List<string>();

    public IList<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
}
