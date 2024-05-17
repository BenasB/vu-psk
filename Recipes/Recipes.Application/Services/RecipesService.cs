using Recipes.Public;
using Recipes.Public.DTOs.Recipes;

namespace Recipes.Application.Services;

public class RecipesService : IRecipesService
{
    public RecipesService()
    {

    }

    public async Task<Recipe> CreateRecipe(RecipeCreationDTO recipe)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteRecipe(int recipeId)
    {
        throw new NotImplementedException();
    }

    public async Task<Recipe> GetRecipe(int recipeId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Recipe>> GetRecipes()
    {
        throw new NotImplementedException();
    }

    public async Task<Recipe> UpdateRecipe(RecipeUpdateDTO recipe)
    {
        throw new NotImplementedException();
    }
}
