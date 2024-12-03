using AuthService.Domain.Entities;

namespace AuthService.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<RefreshToken> RefreshTokens { get; }

        Task<int> SaveAsync();
    }
}
