using Microsoft.EntityFrameworkCore;
using Recipes.DataAccess.Interfaces;
using System.Collections.Generic;
using System;

namespace Recipes.DataAccess.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly RecipesDatabaseContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(RecipesDatabaseContext context, DbSet<T> dbSet)
    {
        _context = context;
        _dbSet = dbSet;
    }

    public async Task DeleteByIdAsync(int id)
    {
        T? entity = await _dbSet.FindAsync(id);

        if(entity == null)
        {
            return;
        }

        if(_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        _dbSet.Remove(entity);
        await SaveAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task InsertAsync(T entity)
    {
        _dbSet.Add(entity);
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;

        await SaveAsync();
    }
}
