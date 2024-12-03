using StatisticsService.Domain.Entities;
using StatisticsService.Infrastructure.Context;

namespace StatisticsService.Infrastructure.Repositories
{
    public class TeamRepository : BaseRepository<Team>
    {
        public TeamRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    }
}
