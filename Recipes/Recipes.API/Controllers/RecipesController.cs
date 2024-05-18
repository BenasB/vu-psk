using Microsoft.AspNetCore.Mvc;
using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Repositories;
using Recipes.Public;

namespace Recipes.API.Controllers;

[ApiController]
[Route("recipes")]
public class RecipesController(IGenericRepository<RecipeEntity> recipesRepository) : ControllerBase
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

        IEnumerable<Recipe> responseRecipes = recipes.Select(recipe => new Recipe { 
            Id = recipe.Id,
            Title = recipe.Title,
            AuthorId = recipe.AuthorId,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            Servings = recipe.Servings,
            UpdatedAt = recipe.UpdatedAt,
            Ingredients = recipe.Ingredients,
            Instructions = recipe.Instructions
        });

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
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if(recipe == null)
        {
            return NotFound();
        }

        Recipe responseRecipe = new Recipe
        {
            Id = recipe.Id,
            Title = recipe.Title,
            AuthorId = recipe.AuthorId,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            Servings = recipe.Servings,
            UpdatedAt = recipe.UpdatedAt,
            Ingredients = recipe.Ingredients,
            Instructions = recipe.Instructions
        };

        return Ok(responseRecipe);
    }

    [HttpDelete("{recipeId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Recipe>> DeleteRecipe(int recipeId)
    {
        RecipeEntity? recipe;

        try
        {
            recipe = await recipesRepository.GetByIdAsync(recipeId);

            if (recipe == null)
            {
                return NotFound();
            }

            recipesRepository.Delete(recipe);
            await recipesRepository.SaveChangesAsync();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok();
    }
}