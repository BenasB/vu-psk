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

    public virtual T? GetById(params object[] id)
    {
        return Context.Set<T>().Find(id);
    }

    public T Insert(T entity)
    {
        var entry = Context.Set<T>().Add(entity);
        Context.Entry(entity).State = EntityState.Added;
        return entry.Entity;
    }

    public T Update(T entity)
    {
        var entry = Context.Set<T>().Update(entity);
        Context.Entry(entity).State = EntityState.Modified;
        return entry.Entity;
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
