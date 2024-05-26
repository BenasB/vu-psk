using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Filtering;

namespace Recipes.DataAccess.Repositories;

public interface IRecipesRepository : IGenericRepository<RecipeEntity>
{
    Task<PaginatedList<RecipeEntity>> GetFiltered(int? top, int? skip, string? name, IList<long>? tags, long? authorId);
}
