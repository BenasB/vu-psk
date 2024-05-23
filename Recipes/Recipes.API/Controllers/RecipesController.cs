using Microsoft.AspNetCore.Mvc;
using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Entities.Relationships;
using Recipes.DataAccess.Repositories;
using Recipes.Public;
using Tags.API.Controllers;

namespace Recipes.API.Controllers;

[ApiController]
[Route("recipes")]
public class RecipesController(IGenericRepository<RecipeEntity> recipesRepository, IGenericRepository<TagRecipeEntity> tagRecipesRepository) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetAllRecipes()
    {
        IEnumerable<RecipeEntity> recipes;
        try
        {
            recipes = await recipesRepository.GetAllAsync();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var responseRecipes = recipes.Select(GetRecipeFromEntity);

        return Ok(responseRecipes);
    }

    [HttpGet("{recipeId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Recipe>> GetRecipe(int recipeId)
    {
        RecipeEntity? recipe;
        try
        {
            recipe = await recipesRepository.GetByIdAsync(recipeId);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (recipe is null) return NotFound();

        var responseRecipe = GetRecipeFromEntity(recipe);

        return Ok(responseRecipe);
    }

    [HttpDelete("{recipeId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteRecipe(int recipeId)
    {
        RecipeEntity? recipe;
        try
        {
            recipe = await recipesRepository.GetByIdAsync(recipeId);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        if (recipe is null) return NotFound();

        try
        {
            recipesRepository.Delete(recipe);
            await recipesRepository.SaveChangesAsync();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }

    [HttpDelete("{recipeId:int}/Tags/{tagId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteRecipeTag(int recipeId, int tagId)
    {
        TagRecipeEntity? recipeTag;
        try
        {
            recipeTag = await tagRecipesRepository.GetByIdAsync(recipeId, tagId);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (recipeTag is null) return NotFound();

        try
        {
            tagRecipesRepository.Delete(recipeTag);
            await tagRecipesRepository.SaveChangesAsync();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }

    private Recipe GetRecipeFromEntity(RecipeEntity recipeEntity)
    {
        return new Recipe
        {
            Id = recipeEntity.Id,
            Title = recipeEntity.Title,
            AuthorId = recipeEntity.AuthorId,
            Description = recipeEntity.Description,
            CookingTime = recipeEntity.CookingTime,
            Servings = recipeEntity.Servings,
            UpdatedAt = recipeEntity.UpdatedAt,
            Ingredients = recipeEntity.Ingredients,
            Instructions = recipeEntity.Instructions,
            Image = recipeEntity.Image
            Tags = recipeEntity.Tags.Select(t => t.Tag).Select(TagsController.GetTagFromEntity),
        };
    }
}