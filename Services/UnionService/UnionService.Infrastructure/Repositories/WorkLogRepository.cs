using UnionService.Domain.Entities;
using UnionService.Infrastructure.Context;

namespace UnionService.Infrastructure.Repositories
{
    public class WorkLogRepository : BaseRepository<WorkLog> 
    {
        public WorkLogRepository(ApplicationContext applicationContext) : base(applicationContext) {}
    }
}
