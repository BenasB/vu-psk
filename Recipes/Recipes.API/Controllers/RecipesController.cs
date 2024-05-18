using Microsoft.AspNetCore.Mvc;
using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Repositories;
using Recipes.Public;
using Recipes.Public.DTO;

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
        
        // Return type currently doesn't match actual type. RecipeEntity should be probably mapped to some RecipeResponse DTO type.

        return Ok(recipes);
    }
}