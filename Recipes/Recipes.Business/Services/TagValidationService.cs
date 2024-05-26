using Recipes.Business.Exceptions;
using Recipes.Business.Services.Interfaces;
using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Entities.Relationships;
using Recipes.DataAccess.Repositories;

namespace Recipes.Business.Services;

public class TagValidationService : ITagValidationService
{
    private readonly IGenericRepository<TagEntity> _tagRepository;

    public TagValidationService(IGenericRepository<TagEntity> tagsRepository)
    {
        _tagRepository = tagsRepository;
    }

    public async Task<IList<TagRecipeEntity>> ValidateTags(RecipeEntity recipeEntity, IList<string> tagNames)
    {
        var allTags = await _tagRepository.GetAllAsync();
        var selectedTags = allTags.Where(s => tagNames.Contains(s.Name)).ToList();

        if (selectedTags.Count != tagNames.Count)
            throw new HttpException("Tag with provided name does not exist", 400);

        return selectedTags
            .Select(x => new TagRecipeEntity { Tag = x, Recipe = recipeEntity })
            .ToList();
    }
}
