using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Recipes.DataAccess.Entities.Relationships;

namespace Recipes.DataAccess.Entities;

public class TagEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required string Name { get; set; }

    public IList<TagRecipeEntity> Recipes { get; set; } = new List<TagRecipeEntity>();
}