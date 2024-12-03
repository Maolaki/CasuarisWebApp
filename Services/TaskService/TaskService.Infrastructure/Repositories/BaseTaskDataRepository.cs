using TaskService.Domain.Entities;
using TaskService.Infrastructure.Context;

namespace TaskService.Infrastructure.Repositories
{
    public class BaseTaskDataRepository : BaseRepository<BaseTaskData> 
    {
        public BaseTaskDataRepository(ApplicationContext applicationContext) : base(applicationContext) {}
    }
}
