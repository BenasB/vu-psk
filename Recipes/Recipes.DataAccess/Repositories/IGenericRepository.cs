using Recipes.DataAccess.Filtering;

namespace Recipes.DataAccess.Repositories;

public interface IGenericRepository<T> where T : class
{
    public Task<PaginatedList<T>> GetAllAsync(int? top = null, int? skip = null);
    public T? GetById(params object[] id);
    public T Insert(T entity);
    public T Update(T entity);
    public void Delete(T entity);
    public Task SaveChangesAsync();
}