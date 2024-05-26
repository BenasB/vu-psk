using Recipes.DataAccess.Entities;

namespace Recipes.Business.Services.Interfaces;

public interface IFeaturedRecipeStrategy
{
    Task<IEnumerable<RecipeEntity>> GetFeaturedRecipesAsync();
}