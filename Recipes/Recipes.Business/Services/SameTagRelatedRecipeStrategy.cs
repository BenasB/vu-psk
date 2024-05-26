using Recipes.Business.Services.Interfaces;
using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Repositories;

namespace Recipes.Business.Services;

public class SameTagRelatedRecipeStrategy(IRecipesRepository recipesRepository) : IRelatedRecipeStrategy
{
    public async Task<IEnumerable<RecipeEntity>> GetRelatedRecipesAsync(RecipeEntity recipe)
    {
        var tasks = recipe.Tags.Select(tag => recipesRepository.GetFiltered(name: null,
            authorId: null,
            tags: [tag.TagId],
            top: null,
            skip: null)).ToList();

        await Task.WhenAll(tasks);

        return tasks
            .Select(t => t.Result.Items)
            .SelectMany(x => x)
            .DistinctBy(x => x.Id)
            .Take(10)
            .ToList();
    }
}