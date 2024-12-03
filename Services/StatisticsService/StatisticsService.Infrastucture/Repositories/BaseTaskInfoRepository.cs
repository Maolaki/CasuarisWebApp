using StatisticsService.Domain.Entities;
using StatisticsService.Infrastructure.Context;

namespace StatisticsService.Infrastructure.Repositories
{
    public class BaseTaskInfoRepository : BaseRepository<BaseTaskInfo>
    {
        public BaseTaskInfoRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    }
}
