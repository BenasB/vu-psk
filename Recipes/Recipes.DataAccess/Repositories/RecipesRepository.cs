using Microsoft.EntityFrameworkCore;
using Recipes.DataAccess.Entities;

namespace Recipes.DataAccess.Repositories;

public class RecipesRepository : GenericRepository<RecipeEntity>
{
    public RecipesRepository(RecipesDatabaseContext context) : base(context) { }

    public override async Task<IEnumerable<RecipeEntity>> GetAllAsync()
    {
        return await Context.Set<RecipeEntity>().Include(s => s.Tags).ThenInclude(s => s.Tag).ToListAsync();
    }

    public override RecipeEntity? GetById(params object[] id)
    {
        return Context.Set<RecipeEntity>().Include(s => s.Tags).ThenInclude(s => s.Tag).FirstOrDefault(s => s.Id == (int)id[0]);
    }
}
