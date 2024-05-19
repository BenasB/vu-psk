using Microsoft.EntityFrameworkCore;

namespace Recipes.DataAccess.Repositories;

public class GenericRepository<T>(RecipesDatabaseContext context) : IGenericRepository<T> where T : class
{
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public void Insert(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public Task SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }
}
