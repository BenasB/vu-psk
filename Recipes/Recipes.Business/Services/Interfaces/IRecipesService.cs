using Recipes.Public;

namespace Recipes.Business.Services.Interfaces;

public interface IRecipesService
{
    IEnumerable<Recipe> GetAllRecipes(string? title, string? csvTags, long? authorId, int? skip = null, int? top = null);
    Recipe GetRecipe(int recipeId);
    Task DeleteRecipe(int recipeId);
    Task<Recipe> CreateRecipe(RecipeCreateDTO request, int authorId);
    Task<Recipe> UpdateRecipe(int recipeId, RecipeUpdateDTO request);
    Task DeleteRecipeTag(int recipeId, int tagId);
}
