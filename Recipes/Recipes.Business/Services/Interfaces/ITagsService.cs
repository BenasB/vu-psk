using Recipes.Public;

namespace Recipes.Business.Services.Interfaces;

public interface ITagsService
{
    Task<PaginatedResponse<Tag>> GetAllTagsAsync(int? skip = null, int? top = null);
    Tag GetTag(int tagId);
    Task DeleteTagAsync(int tagId);
    Task<Tag> CreateTagAsync(TagCreateUpdateDTO request);
}
