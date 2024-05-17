using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Application.DTOs.Recipes;

public class RecipeCreationDTO
{
    public int AuthorId { get; set; }   
    public required string Title { get; set; }
    public string? Description { get; set; }
    public TimeSpan CookingTime { get; set; }
    public int Servings { get; set; }
    public required IList<string> Instructions { get; set; } = new List<string>();
    public required IList<string> Ingredients { get; set; } = new List<string>();
}
