using TaskService.Domain.Entities;
using TaskService.Infrastructure.Context;

namespace TaskService.Infrastructure.Repositories
{
    public class ResourceRepository : BaseRepository<Resource> 
    {
        public ResourceRepository(ApplicationContext applicationContext) : base(applicationContext) {}
    }
}
