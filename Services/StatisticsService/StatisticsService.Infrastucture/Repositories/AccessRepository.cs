using StatisticsService.Domain.Entities;
using StatisticsService.Infrastructure.Context;

namespace StatisticsService.Infrastructure.Repositories
{
    public class AccessRepository : BaseRepository<Access>
    {
        public AccessRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    }
}
