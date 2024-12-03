using AuthService.Domain.Entities;
using AuthService.Infrastructure.Context;

namespace AuthService.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    }
}
