using UnionService.Domain.Entities;
using UnionService.Infrastructure.Context;

namespace UnionService.Infrastructure.Repositories
{
    public class AccessRepository : BaseRepository<Access>
    {
        public AccessRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    }
}
