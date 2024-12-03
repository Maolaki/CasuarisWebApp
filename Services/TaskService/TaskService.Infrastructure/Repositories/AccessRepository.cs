using TaskService.Domain.Entities;
using TaskService.Infrastructure.Context;

namespace TaskService.Infrastructure.Repositories
{
    public class AccessRepository : BaseRepository<Access>
    {
        public AccessRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    }
}
