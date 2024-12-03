using System.Linq.Expressions;

namespace StatisticsService.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
