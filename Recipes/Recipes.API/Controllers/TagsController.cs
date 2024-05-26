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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Tag>>> GetAllTags([FromQuery] int? skip, [FromQuery] int? top)
    {
        return Ok(await tagsService.GetAllTagsAsync(skip, top));
    }

    [HttpGet("{tagId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<Tag> GetTag(int tagId)
    {
        return Ok(tagsService.GetTag(tagId));
    }

    [HttpDelete("{tagId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteTag(int tagId)
    {
        await tagsService.DeleteTagAsync(tagId);
        return NoContent();
    }
}