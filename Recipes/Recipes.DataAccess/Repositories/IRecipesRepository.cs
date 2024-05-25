using Recipes.DataAccess.Entities;

namespace Recipes.DataAccess.Repositories;

public interface IRecipesRepository : IGenericRepository<RecipeEntity>
{
    IEnumerable<RecipeEntity> GetFiltered(int? top, int? skip, string? name, IList<long>? tags, long? authorId);
}
