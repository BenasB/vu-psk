namespace Recipes.DataAccess.Repositories;

public interface IGenericRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();
    public T? GetById(params object[] id);
    public T Insert(T entity);
    public T Update(T entity);
    public void Delete(T entity);
    public Task SaveChangesAsync();
}