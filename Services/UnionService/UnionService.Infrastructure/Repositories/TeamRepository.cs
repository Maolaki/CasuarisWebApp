using UnionService.Domain.Entities;
using UnionService.Infrastructure.Context;

namespace UnionService.Infrastructure.Repositories
{
    public class TeamRepository : BaseRepository<Team>
    {
        public TeamRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    }
}
