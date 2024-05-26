using Microsoft.EntityFrameworkCore;
using Recipes.DataAccess.Entities.Relationships;
using Recipes.DataAccess.Repositories;
using Recipes.Public;
using Recipes.Business.Exceptions;
using Recipes.Business.Services.Interfaces;

namespace Recipes.Business.Services;

public class RecipesService : IRecipesService
{
    private readonly IRecipesRepository _recipesRepository;
    private readonly IGenericRepository<TagRecipeEntity> _tagRecipeRepository;
    private readonly ITagValidationService _tagValidationService;

    public RecipesService(
        IRecipesRepository recipesRepository,
        IGenericRepository<TagRecipeEntity> tagRecipeRepository,
        ITagValidationService tagValidationService)
    {
        _recipesRepository = recipesRepository;
        _tagRecipeRepository = tagRecipeRepository;
        _tagValidationService = tagValidationService;
    }

    public async Task<PaginatedResponse<Recipe>> GetAllRecipes(string? title, string? csvTags, long? authorId, int? skip = null, int? top = null)
    {
        IList<long>? tags = TryParseTags(csvTags);

        var recipes = await _recipesRepository.GetFiltered(top, skip, title, tags, authorId);
        return MappingService.GetPaginatedResponse(recipes, MappingService.GetRecipeFromEntity);

    public Recipe GetRecipe(int recipeId)
    {
        var recipe = _recipesRepository.GetById(recipeId);
        if (recipe is null)
            throw new HttpException("Recipe not found", 404);

        return MappingService.GetRecipeFromEntity(recipe);
    }

    // TODO: Check if user is admin
    public async Task DeleteRecipe(int recipeId)
    {
        var recipe = _recipesRepository.GetById(recipeId);
        if (recipe is null)
            throw new HttpException("Recipe not found", 404);

        _recipesRepository.Delete(recipe);
        await _recipesRepository.SaveChangesAsync();
    }

    public async Task<Recipe> CreateRecipe(RecipeCreateDTO request, int authorId)
    {
        var newRecipe = MappingService.GetRecipeFromDTO(request, authorId);
        var tags = await _tagValidationService.ValidateTags(newRecipe, request.Tags);
        newRecipe.Tags = tags.ToList();

        newRecipe = _recipesRepository.Insert(newRecipe);
        await _recipesRepository.SaveChangesAsync();

        return MappingService.GetRecipeFromEntity(newRecipe);
    }

    // TODO: Check if user is admin
    public async Task<Recipe> UpdateRecipe(int recipeId, RecipeUpdateDTO request)
    {
        var recipeToUpdate = _recipesRepository.GetById(recipeId);
        if (recipeToUpdate == null)
            throw new HttpException("Recipe not found", 404);

        recipeToUpdate = MappingService.UpdateOldRecipeEntity(recipeToUpdate, request);
        var tags = await _tagValidationService.ValidateTags(recipeToUpdate, request.Tags);
        recipeToUpdate.Tags = tags.ToList();

        recipeToUpdate = _recipesRepository.Update(recipeToUpdate);
        await SaveWithConcurrencyCheck();

        return MappingService.GetRecipeFromEntity(recipeToUpdate);
    }

    // TODO: Check if user is admin
    public async Task DeleteRecipeTag(int recipeId, int tagId)
    {
        var recipeTag = _tagRecipeRepository.GetById(recipeId, tagId);
        if (recipeTag is null) throw new HttpException("Recipe tag not found", 404);

        _tagRecipeRepository.Delete(recipeTag);
        await _tagRecipeRepository.SaveChangesAsync();
    }

    private IList<long> TryParseTags(string? csvTags)
    {
        try
        {
            return csvTags?.Split(',').Select(long.Parse).ToList();
        }
        catch
        {
            throw new HttpException("Invalid tags format", 400);
        }
    }

    private async Task SaveWithConcurrencyCheck()
    {
        try
        {
            await _recipesRepository.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new HttpException("Recipe was modified by another user.", 409);
        }
    }
}
