using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskService.Domain.Interfaces;
using TaskService.Infrastructure.Context;

namespace TaskService.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _applicationContext;
        protected DbSet<T> _dbSet;

        public BaseRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _dbSet = _applicationContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize)
        {
            return await _dbSet
                .Where(predicate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
        }

        public virtual void Update(T item)
        {
            _dbSet.Update(item);
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }
    }
}
