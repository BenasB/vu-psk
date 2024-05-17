using Microsoft.EntityFrameworkCore;
using Recipes.DataAccess.Interfaces;

namespace Recipes.DataAccess.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext dbContext, DbSet<T> dbSet)
    {
        _dbContext = dbContext;
        _dbSet = dbSet;
    }

    public async Task<T> CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<T> DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<T> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}
