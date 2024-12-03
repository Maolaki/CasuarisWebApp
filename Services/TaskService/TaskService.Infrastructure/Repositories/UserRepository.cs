using TaskService.Domain.Entities;
using TaskService.Infrastructure.Context;

namespace TaskService.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    }
}
