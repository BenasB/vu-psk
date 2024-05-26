using Recipes.Business.Services.Interfaces;
using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Entities.Relationships;
using Recipes.DataAccess.Repositories;

namespace Recipes.Business.Services;

public class TagCreationService : ITagValidationService
{
    private readonly IGenericRepository<TagEntity> _tagRepository;

    public TagCreationService(IGenericRepository<TagEntity> tagsRepository)
    {
        _tagRepository = tagsRepository;
    }

    public async Task<IList<TagRecipeEntity>> ValidateTags(RecipeEntity recipeEntity, IList<string> tagNames)
    {
        await InsertNewTags(tagNames);
        return await GetRecipeTags(recipeEntity, tagNames);
    }


    private async Task InsertNewTags(IList<string> allTagNames)
    {
        if (!allTagNames.Any())
            return;

        var allTags = await _tagRepository.GetAllAsync(int.MaxValue);
        var existingTags = allTags.Items.Where(s => allTagNames.Contains(s.Name));
        var newTagsNamesList = allTagNames.Except(existingTags.Select(x => x.Name)).ToList();

        foreach (var tagName in newTagsNamesList)
            _tagRepository.Insert(new TagEntity { Name = tagName });
    }

    private async Task<IList<TagRecipeEntity>> GetRecipeTags(RecipeEntity recipeEntity, IList<string> allTagNames)
    {
        var allTags = await _tagRepository.GetAllAsync(int.MaxValue);

        return allTags.Items
            .Where(x => allTagNames.Contains(x.Name))
            .Select(x => new TagRecipeEntity { Tag = x, Recipe = recipeEntity })
            .ToList();
    }
}
