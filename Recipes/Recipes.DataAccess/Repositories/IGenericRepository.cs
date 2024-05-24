namespace Recipes.DataAccess.Repositories;

public interface IGenericRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();
    public IQueryable<T> GetQueryable();
    public T? GetById(params object[] id);
    public void Insert(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public Task SaveChangesAsync();
}