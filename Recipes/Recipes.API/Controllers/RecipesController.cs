using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Entities.Relationships;
using Recipes.DataAccess.Repositories;
using Recipes.Public;

namespace Recipes.API.Controllers;

[ApiController]
[Route("recipes")]
public class RecipesController : ControllerBase
{
    private readonly IGenericRepository<RecipeEntity> _recipesRepository;
    private readonly IGenericRepository<TagRecipeEntity> _tagRecipeRepository;
    private readonly IGenericRepository<TagEntity> _tagRepository;
    
    public RecipesController(
        IGenericRepository<RecipeEntity> recipeRepository,
        IGenericRepository<TagRecipeEntity> tagRecipeRepository,
        IGenericRepository<TagEntity> tagRepository)
    {
        _tagRecipeRepository = tagRecipeRepository;
        _recipesRepository = recipeRepository;
        _tagRepository = tagRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetAllRecipes()
    {
        IEnumerable<RecipeEntity> recipes;
        try
        {
            recipes = await _recipesRepository.GetAllAsync();
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
    public ActionResult<Recipe> GetRecipe(int recipeId)
    {
        RecipeEntity? recipe;
        try
        {
            recipe = _recipesRepository.GetById(recipeId);
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
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteRecipe(int recipeId)
    {
        RecipeEntity? recipe;
        try
        {
            recipe = _recipesRepository.GetById(recipeId);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        if (recipe is null) return NotFound();
        
        // TODO: Check if user is author

        try
        {
            _recipesRepository.Delete(recipe);
            await _recipesRepository.SaveChangesAsync();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateRecipe(RecipeCreateDTO request)
    {
        try
        {
            await InsertNewTags(request.Tags);

            var authorId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var newRecipe = GetRecipeFromDTO(request, authorId);
            newRecipe.Tags = await GetEntityTags(newRecipe, request.Tags);

            newRecipe = _recipesRepository.Insert(newRecipe);
            await _recipesRepository.SaveChangesAsync();

            var response = GetRecipeFromEntity(newRecipe);

            return Created($"/recipes/{response.Id}", response);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{recipeId:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateRecipe(int recipeId, [FromBody] RecipeUpdateDTO request)
    {
        RecipeEntity? recipeToUpdate;

        try
        {
            recipeToUpdate = _recipesRepository.GetById(recipeId);

            if (recipeToUpdate == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            
            // TODO: Check if user is author

            await InsertNewTags(request.Tags);

            recipeToUpdate = UpdateOldRecipeEntity(recipeToUpdate, request);
            recipeToUpdate.Tags = await GetEntityTags(recipeToUpdate, request.Tags);

            recipeToUpdate = _recipesRepository.Update(recipeToUpdate);
            await _recipesRepository.SaveChangesAsync();

            var response = GetRecipeFromEntity(recipeToUpdate);

            return Ok(response);
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(StatusCodes.Status409Conflict, "Recipe was modified by another user.");
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }

    [HttpDelete("{recipeId:int}/tags/{tagId:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteRecipeTag(int recipeId, int tagId)
    {
        TagRecipeEntity? recipeTag;
        try
        {
            recipeTag = _tagRecipeRepository.GetById(recipeId, tagId);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (recipeTag is null) return NotFound();
        
        // TODO: Look up authorId and check if user is authorId

        try
        {
            _tagRecipeRepository.Delete(recipeTag);
            await _tagRecipeRepository.SaveChangesAsync();
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
            Tags = recipeEntity.Tags.Select(t => t.Tag).Select(TagsController.GetTagFromEntity),
            Version = recipeEntity.RowVersion,
        };
    }

    private RecipeEntity UpdateOldRecipeEntity(RecipeEntity oldRecipeEntity, RecipeUpdateDTO recipeDTO)
    {
        oldRecipeEntity.Title = recipeDTO.Title;
        oldRecipeEntity.AuthorId = recipeDTO.AuthorId;
        oldRecipeEntity.Description = recipeDTO.Description;
        oldRecipeEntity.CookingTime = recipeDTO.CookingTime;
        oldRecipeEntity.Servings = recipeDTO.Servings;
        oldRecipeEntity.Ingredients = recipeDTO.Ingredients;
        oldRecipeEntity.Instructions = recipeDTO.Instructions;
        oldRecipeEntity.Image = recipeDTO.Image;
        oldRecipeEntity.RowVersion = recipeDTO.Version;

        return oldRecipeEntity;
    }

    private RecipeEntity GetRecipeFromDTO(RecipeCreateDTO recipeDTO, int authorId)
    {
        return new RecipeEntity()
        {
            Title = recipeDTO.Title,
            AuthorId = authorId,
            Description = recipeDTO.Description,
            CookingTime = recipeDTO.CookingTime,
            Servings = recipeDTO.Servings,
            Ingredients = recipeDTO.Ingredients,
            Instructions = recipeDTO.Instructions,
            Image = recipeDTO.Image
        };
    }

    private async Task InsertNewTags(IList<string> allTagNames)
    {
        if (allTagNames == null || allTagNames.Count == 0) return;

        var allTags = await _tagRepository.GetAllAsync();
        var existingTags = new List<TagEntity>();

        foreach (var tag in allTags)
        {
            if (allTagNames.Contains(tag.Name))
            {
                existingTags.Add(tag);
            }
        }

        List<string> newTags = allTagNames.Except(existingTags.Select(x => x.Name)).ToList();

        foreach (var tagName in newTags)
        {
            _tagRepository.Insert(new TagEntity { Name = tagName });
        }

        await _recipesRepository.SaveChangesAsync();
    }

    private async Task<List<TagRecipeEntity>> GetEntityTags(RecipeEntity recipeEntity, IList<string> allTagNames)
    {
        var allTags = await _tagRepository.GetAllAsync();

        return allTags
                .Where(x => allTagNames.Contains(x.Name))
                .ToHashSet()
                .Select(x => new TagRecipeEntity { Tag = x, Recipe = recipeEntity })
                .ToList();
    }
}