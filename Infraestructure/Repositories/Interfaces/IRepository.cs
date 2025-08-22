using System.Linq.Expressions;

namespace WorkoutTrackerApi.Infraestructure.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // Basic methods CRUD
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> DeleteByIdAsync(int id);

        Task<T?> GetByConditionAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetManyByConditionAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

   
        Task<T?> GetByIdWithIncludesAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetManyWithIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    }
}