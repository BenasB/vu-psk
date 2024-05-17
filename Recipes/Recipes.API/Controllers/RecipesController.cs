using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Services;

namespace Recipes.API.Controllers;

[ApiController]
[Route("recipes")]
public class RecipesController : ControllerBase
{
    private IRecipesService _recipesService;

    public RecipesController(IRecipesService recipesService)
    {
        _recipesService = recipesService;
    }

}
