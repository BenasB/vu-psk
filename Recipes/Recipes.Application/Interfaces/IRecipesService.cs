using Recipes.Public;
using Recipes.Public.DTOs.Recipes;

namespace Recipes.Application.Services;

public interface IRecipesService
{
    public Task<Recipe> GetRecipe(int recipeId);
    public Task<IEnumerable<Recipe>> GetRecipes();
    public Task<Recipe> CreateRecipe(RecipeCreationDTO recipe);
    public Task<Recipe> UpdateRecipe(RecipeUpdateDTO recipe);
    public Task DeleteRecipe(int recipeId);
}
