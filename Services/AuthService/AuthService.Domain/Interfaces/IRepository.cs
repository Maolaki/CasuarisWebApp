using System.Linq.Expressions;

namespace AuthService.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
