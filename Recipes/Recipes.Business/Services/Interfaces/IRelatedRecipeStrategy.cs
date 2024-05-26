using Recipes.DataAccess.Entities;

namespace Recipes.Business.Services.Interfaces;

public interface IRelatedRecipeStrategy
{
    Task<IEnumerable<RecipeEntity>> GetRelatedRecipesAsync(RecipeEntity recipe);
}