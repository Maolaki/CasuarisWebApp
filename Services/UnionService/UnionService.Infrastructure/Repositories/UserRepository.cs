using UnionService.Domain.Entities;
using UnionService.Infrastructure.Context;

namespace UnionService.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    }
}
