using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Business.Services.Interfaces;
using Recipes.Public;

namespace Recipes.API.Controllers;

[ApiController]
[Route("recipes")]
public class RecipesController(IRecipesService recipeService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedResponse<Recipe>>> GetAllRecipes([FromQuery] string? title, [FromQuery] string? csvTags, [FromQuery] long? authorId, [FromQuery] int? skip, [FromQuery] int? top)
    {
        return Ok(await recipeService.GetAllRecipes(title, csvTags, authorId, skip, top));
    }

    [HttpGet("{recipeId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<Recipe> GetRecipe(int recipeId)
    {
        return Ok(recipeService.GetRecipe(recipeId));
    }

    [HttpDelete("{recipeId:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteRecipe(int recipeId)
    {
        await recipeService.DeleteRecipe(recipeId);
        return NoContent();
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateRecipe(RecipeCreateDTO request)
    {
        var authorId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var response = await recipeService.CreateRecipe(request, authorId);
        return Created($"/recipes/{response.Id}", response);
    }

    [HttpPut("{recipeId:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateRecipe(int recipeId, [FromBody] RecipeUpdateDTO request)
    {
        return Ok(await recipeService.UpdateRecipe(recipeId, request));
    }

    [HttpDelete("{recipeId:int}/tags/{tagId:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteRecipeTag(int recipeId, int tagId)
    {
        await recipeService.DeleteRecipeTag(recipeId, tagId);
        return NoContent();
    }
}