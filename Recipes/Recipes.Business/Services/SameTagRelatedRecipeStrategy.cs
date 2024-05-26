using Recipes.Business.Services.Interfaces;
using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Repositories;

namespace Recipes.Business.Services;

public class SameTagRelatedRecipeStrategy(IRecipesRepository recipesRepository) : IRelatedRecipeStrategy
{
    public async Task<IEnumerable<RecipeEntity>> GetRelatedRecipesAsync(RecipeEntity recipe)
    {
        var joinedRecipes = new List<RecipeEntity>();
        foreach (var tag in recipe.Tags)
        {
            var result = await recipesRepository.GetFiltered(name: null,
                authorId: null,
                tags: [tag.TagId],
                top: null,
                skip: null);
            
            joinedRecipes = joinedRecipes.Concat(result.Items.ToList()).ToList();
        }
        

        return joinedRecipes
            .DistinctBy(x => x.Id)
            .Where(x => x.Id != recipe.Id)
            .Take(10)
            .ToList();
    }
}