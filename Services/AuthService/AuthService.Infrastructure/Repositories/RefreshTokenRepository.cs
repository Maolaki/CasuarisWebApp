using AuthService.Domain.Entities;
using AuthService.Infrastructure.Context;

namespace AuthService.Infrastructure.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>
    {
        public RefreshTokenRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    }
}
