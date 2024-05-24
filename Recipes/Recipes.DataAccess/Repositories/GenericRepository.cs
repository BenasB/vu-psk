using Microsoft.EntityFrameworkCore;

namespace Recipes.DataAccess.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly RecipesDatabaseContext Context;

    public GenericRepository(RecipesDatabaseContext context)
    {
        Context = context;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public virtual IQueryable<T> GetQueryable()
    {
        return Context.Set<T>();
    }

    public virtual T? GetById(params object[] id)
    {
        return Context.Set<T>().Find(id);
    }

    public void Insert(T entity)
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
