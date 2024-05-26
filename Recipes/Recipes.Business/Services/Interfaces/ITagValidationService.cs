using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Entities.Relationships;

namespace Recipes.Business.Services.Interfaces;

public interface ITagValidationService
{
    Task<IList<TagRecipeEntity>> ValidateTags(RecipeEntity recipeEntity, IList<string> tagNames);
}
