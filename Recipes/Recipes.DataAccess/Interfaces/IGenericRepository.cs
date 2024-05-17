namespace Recipes.DataAccess.Interfaces;

public interface IGenericRepository<T> where T : class
{
    public Task<T> CreateAsync(T entity);
    public Task<T> GetByIdAsync(int id);
    public Task<T> GetAllAsync();
    public Task<T> DeleteByIdAsync(int id);
    public Task SaveAsync();
}
