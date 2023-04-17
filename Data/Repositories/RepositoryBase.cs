using circle_coordinator.Data.Context;
using circle_coordinator.Data.Repositories.Interfaces;
using circle_coordinator.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace circle_coordinator.Data.Repositories;

public class RepositoryBase<T> : IRepository<T> where T : EntityBase
{
	protected readonly CCDbContext _context;
	protected readonly DbSet<T> _dbSet;

	protected RepositoryBase(CCDbContext context)
	{
		_context = context;
		_dbSet = context.Set<T>();
	}

	public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
	public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

	public async Task<T?> AddAsync(T entity)
	{
		try
		{
			var result = await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();
			return result.Entity;
		}
		catch (Exception e)
		{
			await LogDatabaseError(e);
			return null;
		}
	}

	public async Task<T?> UpdateAsync(T entity)
	{
		var existingEntity = await _dbSet.FindAsync(entity.Id);

		if (existingEntity == null)
		{
			return null;
		}

		try
		{
			_context.Entry(existingEntity).CurrentValues.SetValues(entity);
			await _context.SaveChangesAsync();

			return existingEntity;
		}
		catch (Exception e)
		{
			await LogDatabaseError(e);
			return null;
		}
	}

	public async Task<T?> DeleteAsync(T entity)
	{
		try
		{
			_dbSet.Remove(entity);
			await _context.SaveChangesAsync();
			return entity;
		}
		catch (Exception e)
		{
			await LogDatabaseError(e);
			return null;
		}
	}

	protected Task LogDatabaseError(Exception? exception)
	{
		Log.Error(exception, "An error occurred during a database operation");
		return Task.CompletedTask;
	}
}