namespace Recipes.DataAccess.Interfaces;

public interface IGenericRepository<T> where T : class
{
    public Task InsertAsync(T entity);
    public Task<T> GetByIdAsync(int id);
    public Task UpdateAsync(T entity);
    public Task<IEnumerable<T>> GetAllAsync();
    public Task DeleteByIdAsync(int id);
    public Task SaveAsync();
}
