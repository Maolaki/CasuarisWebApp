using StatisticsService.Domain.Entities;
using StatisticsService.Infrastructure.Context;

namespace StatisticsService.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    }
}
