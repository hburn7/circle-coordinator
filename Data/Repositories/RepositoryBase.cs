using circle_coordinator.Data.Context;
using circle_coordinator.Data.Repositories.Interfaces;
using circle_coordinator.Models;
using Microsoft.EntityFrameworkCore;

namespace circle_coordinator.Data.Repositories;

public class RepositoryBase<T> : IRepository<T> where T : EntityBase
{
	protected readonly CCDbContext _context;
	protected readonly DbSet<T> _dbSet;
	
	public RepositoryBase(CCDbContext context)
	{
		_context = context;
		_dbSet = context.Set<T>();
	}
	
	public async Task<IEnumerable<T>> GetAllAsync()
	{
		return await _dbSet.ToListAsync();
	}

	public async Task<T> GetByIdAsync(int id)
	{
		return await _dbSet.FindAsync(id);
	}

	public async Task<T> AddAsync(T entity)
	{
		var result = await _dbSet.AddAsync(entity);
		await _context.SaveChangesAsync();
		return result.Entity;
	}

	public async Task<T> UpdateAsync(T entity)
	{
		var existingEntity = await _dbSet.FindAsync(entity.Id);

		if (existingEntity == null)
		{
			return null;
		}

		_context.Entry(existingEntity).CurrentValues.SetValues(entity);
		await _context.SaveChangesAsync();

		return existingEntity;
	}

	public async Task<T> DeleteAsync(T entity)
	{
		_dbSet.Remove(entity);
		await _context.SaveChangesAsync();
		return entity;
	}
}