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

}
