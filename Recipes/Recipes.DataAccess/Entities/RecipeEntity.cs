using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Recipes.DataAccess.Entities.Relationships;

namespace Recipes.DataAccess.Entities;

public class RecipeEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int AuthorId { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public TimeSpan CookingTime { get; set; }

    public int Servings { get; set; }

    public DateTime UpdatedAt { get; set; }

    public required IList<string> Instructions { get; set; } = new List<string>();

    public required IList<string> Ingredients { get; set; } = new List<string>();
    
    public string? Image { get; set; }

    public List<TagRecipeEntity> Tags { get; set; } = new();
}
