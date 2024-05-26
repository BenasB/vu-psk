using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Repositories;
using Recipes.Public;
using Recipes.Business.Exceptions;
using Recipes.Business.Services.Interfaces;

namespace Recipes.Business.Services;

public class TagsService(IGenericRepository<TagEntity> tagsRepository) : ITagsService
{
    public async Task<PaginatedResponse<Tag>> GetAllTagsAsync(int? skip = null, int? top = null)
    {
        var tags = await tagsRepository.GetAllAsync(top, skip);
        return MappingService.GetPaginatedResponse(tags, MappingService.GetTagFromEntity);
    }

    public Tag GetTag(int tagId)
    {
        var tag = tagsRepository.GetById(tagId);
        if (tag is null)
            throw new HttpException("Tag not found", 404);

        return MappingService.GetTagFromEntity(tag);
    }

    public async Task DeleteTagAsync(int tagId)
    {
        var tag = tagsRepository.GetById(tagId);
        if (tag is null)
            throw new HttpException("Tag not found", 404);

        tagsRepository.Delete(tag);
        await tagsRepository.SaveChangesAsync();
    }

    public async Task<Tag> CreateTagAsync(TagCreateUpdateDTO request)
    {
        var newTag = MappingService.GetTagFromDTO(request);
        newTag = tagsRepository.Insert(newTag);
        await tagsRepository.SaveChangesAsync();

        return MappingService.GetTagFromEntity(newTag);
    }
}
