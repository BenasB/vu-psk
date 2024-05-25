using Microsoft.EntityFrameworkCore;
using Recipes.DataAccess.Filtering;

namespace Recipes.DataAccess.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly RecipesDatabaseContext Context;

    public GenericRepository(RecipesDatabaseContext context)
    {
        Context = context;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(int? top = null, int? skip = null)
    {
        return await Context.Set<T>().Paginate(top, skip).ToListAsync();
    }

    public virtual T? GetById(params object[] id)
    {
        return Context.Set<T>().Find(id);
    }

    public virtual void Insert(T entity)
    {
        Context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        Context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
    }

    public Task SaveChangesAsync()
    {
        return Context.SaveChangesAsync();
    }
}
