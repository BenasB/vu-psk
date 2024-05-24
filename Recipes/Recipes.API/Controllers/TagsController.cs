using Microsoft.AspNetCore.Mvc;
using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Repositories;
using Recipes.Public;

namespace Recipes.API.Controllers;

[ApiController]
[Route("tags")]
public class TagsController(IGenericRepository<TagEntity> tagsRepository) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Tag>>> GetAllTags()
    {
        IEnumerable<TagEntity> tags;
        try
        {
            tags = await tagsRepository.GetAllAsync();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var responseTags = tags.Select(GetTagFromEntity);

        return Ok(responseTags);
    }

    [HttpGet("{tagId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<Tag> GetTag(int tagId)
    {
        TagEntity? tag;
        try
        {
            tag = tagsRepository.GetById(tagId);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (tag is null) return NotFound();

        var responseTag = GetTagFromEntity(tag);

        return Ok(responseTag);
    }

    [HttpDelete("{tagId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteTag(int tagId)
    {
        TagEntity? tag;
        try
        {
            tag = tagsRepository.GetById(tagId);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        if (tag is null) return NotFound();

        try
        {
            tagsRepository.Delete(tag);
            await tagsRepository.SaveChangesAsync();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }

    public static Tag GetTagFromEntity(TagEntity tagEntity)
    {
        return new Tag
        {
            Id = tagEntity.Id,
            Name = tagEntity.Name,
        };

    }
}