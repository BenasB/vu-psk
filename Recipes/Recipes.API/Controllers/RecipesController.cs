using Microsoft.AspNetCore.Mvc;
using Recipes.DataAccess.Interfaces;
using Recipes.Public;

namespace Recipes.API.Controllers;

[ApiController]
[Route("recipes")]
public class RecipesController : ControllerBase
{
    private readonly IGenericRepository<Recipe> _recipesRepository;

    public RecipesController(IGenericRepository<Recipe> recipesRepository)
    {
        _recipesRepository = recipesRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetAllRecipes()
    {
        var recipes = await _recipesRepository.GetAllAsync();

        if (recipes == null || !recipes.Any())
        {
            return NotFound();
        }

        return Ok(recipes);
    }

}
