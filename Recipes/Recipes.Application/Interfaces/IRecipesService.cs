using Recipes.Application.DTOs.Recipes;
using Recipes.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Application.Services;

public interface IRecipesService
{
    public Task<Recipe> GetRecipe(int recipeId);
    public Task<IEnumerable<Recipe>> GetRecipes();
    public Task<Recipe> CreateRecipe(RecipeCreationDTO recipe);
    public Task<Recipe> UpdateRecipe(RecipeUpdateDTO recipe);
    public Task DeleteRecipe(int recipeId);
}
