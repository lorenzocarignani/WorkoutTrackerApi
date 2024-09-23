using System.Linq.Expressions;

namespace WorkoutTrackerApi.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<T?> GetByCondition(Expression<Func<T, bool>> predicate);
    }
}
