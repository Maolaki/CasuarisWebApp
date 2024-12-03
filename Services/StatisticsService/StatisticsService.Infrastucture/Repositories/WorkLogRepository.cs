using StatisticsService.Domain.Entities;
using StatisticsService.Infrastructure.Context;

namespace StatisticsService.Infrastructure.Repositories
{
    public class WorkLogRepository : BaseRepository<WorkLog> 
    {
        public WorkLogRepository(ApplicationContext applicationContext) : base(applicationContext) {}
    }
}
