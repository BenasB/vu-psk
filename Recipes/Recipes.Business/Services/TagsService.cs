using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Repositories;
using Recipes.Public;
using Recipes.Business.Exceptions;
using Recipes.Business.Services.Interfaces;

namespace Recipes.Business.Services;

public class TagsService : ITagsService
{
    private readonly IGenericRepository<TagEntity> _tagsRepository;

    public TagsService(IGenericRepository<TagEntity> tagsRepository)
    {
        _tagsRepository = tagsRepository;
    }

    public async Task<PaginatedResponse<Tag>> GetAllTagsAsync(int? skip = null, int? top = null)
    {
        var tags = await _tagsRepository.GetAllAsync(top, skip);
        return MappingService.GetPaginatedResponse(tags, MappingService.GetTagFromEntity);
    }

    public Tag GetTag(int tagId)
    {
        var tag = _tagsRepository.GetById(tagId);
        if (tag is null)
            throw new HttpException("Tag not found", 404);

        return MappingService.GetTagFromEntity(tag);
    }

    public async Task DeleteTagAsync(int tagId)
    {
        var tag = _tagsRepository.GetById(tagId);
        if (tag is null)
            throw new HttpException("Tag not found", 404);

        _tagsRepository.Delete(tag);
        await _tagsRepository.SaveChangesAsync();
    }
}
