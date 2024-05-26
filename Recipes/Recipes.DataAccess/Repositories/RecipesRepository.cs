using Microsoft.EntityFrameworkCore;
using Recipes.DataAccess.Entities;
using Recipes.DataAccess.Filtering;

namespace Recipes.DataAccess.Repositories;

public class RecipesRepository : GenericRepository<RecipeEntity>, IRecipesRepository
{
    public RecipesRepository(RecipesDatabaseContext context) : base(context) { }

    public override async Task<PaginatedList<RecipeEntity>> GetAllAsync(int? top = null, int? skip = null)
    {
        return await Context.Set<RecipeEntity>().Include(s => s.Tags).ThenInclude(s => s.Tag).Paginate(top, skip);
    }

    public override RecipeEntity? GetById(params object[] id)
    {
        return Context.Set<RecipeEntity>().Include(s => s.Tags).ThenInclude(s => s.Tag).FirstOrDefault(s => s.Id == (int)id[0]);
    }

    public async Task<PaginatedList<RecipeEntity>> GetFiltered(int? top, int? skip, string? name, IList<long>? tags, long? authorId)
    {
        return await Context.Set<RecipeEntity>()
            .Include(s => s.Tags)
            .ThenInclude(s => s.Tag)
            .FilterByAuthor(authorId)
            .FilterByTags(tags)
            .FilterByTitle(name)
            .Paginate(top, skip);
    }
}
