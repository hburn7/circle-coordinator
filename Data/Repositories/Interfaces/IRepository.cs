using circle_coordinator.Models;

namespace circle_coordinator.Data.Repositories.Interfaces;

public interface IRepository<T> where T : EntityBase
{
	public Task<IEnumerable<T>> GetAllAsync();
	public Task<T?> GetByIdAsync(int id);
	public Task<T?> AddAsync(T entity);
	public Task<T?> UpdateAsync(T entity);
	public Task<T?> DeleteAsync(T entity);
	public Task AddRangeAsync(IEnumerable<T> entities);
}