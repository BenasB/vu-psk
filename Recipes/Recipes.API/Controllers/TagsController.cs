using Identity.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Business.Services.Interfaces;
using Recipes.Public;

namespace Recipes.API.Controllers;

[ApiController]
[Route("tags")]
public class TagsController(ITagsService tagsService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedResponse<Tag>>> GetAllTags([FromQuery] int? skip, [FromQuery] int? top)
    {
        return Ok(await tagsService.GetAllTagsAsync(skip, top));
    }

    [HttpGet("{tagId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<Tag> GetTag(int tagId)
    {
        return Ok(tagsService.GetTag(tagId));
    }

    [HttpDelete("{tagId:int}")]
    [Authorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteTag(int tagId)
    {
        await tagsService.DeleteTagAsync(tagId);
        return NoContent();
    }
    
    [HttpPost]
    [Authorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Tag>> CreateTag(TagCreateUpdateDTO request)
    {
        var response = await tagsService.CreateTagAsync(request);
        return Created($"/tags/{response.Id}", response);
    }
}