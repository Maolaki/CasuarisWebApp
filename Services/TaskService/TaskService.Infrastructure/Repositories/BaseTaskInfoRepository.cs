using TaskService.Domain.Entities;
using TaskService.Infrastructure.Context;

namespace TaskService.Infrastructure.Repositories
{
    public class BaseTaskInfoRepository : BaseRepository<BaseTaskInfo> 
    {
        public BaseTaskInfoRepository(ApplicationContext applicationContext) : base(applicationContext) {}
    }
}
