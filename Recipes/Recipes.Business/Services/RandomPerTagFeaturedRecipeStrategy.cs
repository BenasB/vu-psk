using Recipes.Business.Services.Interfaces;
using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Repositories;

namespace Recipes.Business.Services;

public class RandomPerTagFeaturedRecipeStrategy(IRecipesRepository recipesRepository, IGenericRepository<TagEntity> tagRepository) : IFeaturedRecipeStrategy
{
    public async Task<IEnumerable<RecipeEntity>> GetFeaturedRecipesAsync()
    {
        var allTags = (await tagRepository.GetAllAsync()).Items;
        var featuredRecipes = new List<RecipeEntity>();
        foreach (var tag in allTags)
        {
            var result = await recipesRepository.GetFiltered(name: null,
                authorId: null,
                tags: [tag.Id],
                top: 5,
                skip: null);

            var recipeToFeature = result.Items.FirstOrDefault(recipe => featuredRecipes.All(x => x.Id != recipe.Id));

            if (recipeToFeature != null)
                featuredRecipes.Add(recipeToFeature);
        }

        return featuredRecipes.Take(10).ToList();
    }
}