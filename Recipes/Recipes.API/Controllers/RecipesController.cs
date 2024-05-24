using Microsoft.AspNetCore.Mvc;
using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Entities.Relationships;
using Recipes.DataAccess.Filtering;
using Recipes.DataAccess.Repositories;
using Recipes.Public;

namespace Recipes.API.Controllers;

[ApiController]
[Route("recipes")]
public class RecipesController(IGenericRepository<RecipeEntity> recipesRepository, IGenericRepository<TagRecipeEntity> tagRecipesRepository) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<Recipe>> GetAllRecipes([FromQuery] string? title, [FromQuery] string? csvTags, [FromQuery] long? authorId, [FromQuery] int? skip, [FromQuery] int? top)
    {
        IQueryable<RecipeEntity> recipesQuery;
        try
        {
            recipesQuery = recipesRepository.GetQueryable();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        long[]? tags;
        try
        {
            tags = csvTags?.Split(',').Select(long.Parse).ToArray();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        var recipes = recipesQuery
            .FilterByAuthor(authorId)
            .FilterByTags(tags)
            .FilterByTitle(title)
            .Paginate(top, skip)
            .ToList();

        var responseRecipes = recipes.Select(GetRecipeFromEntity);

        return Ok(responseRecipes);
    }

    [HttpGet("{recipeId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<Recipe> GetRecipe(int recipeId)
    {
        RecipeEntity? recipe;
        try
        {
            recipe = recipesRepository.GetById(recipeId);
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
            recipe = recipesRepository.GetById(recipeId);
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

    [HttpDelete("{recipeId:int}/tags/{tagId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteRecipeTag(int recipeId, int tagId)
    {
        TagRecipeEntity? recipeTag;
        try
        {
            recipeTag = tagRecipesRepository.GetById(recipeId, tagId);
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
            Image = recipeEntity.Image,
            Tags = recipeEntity.Tags.Select(t => t.Tag).Select(TagsController.GetTagFromEntity)
        };
    }
}