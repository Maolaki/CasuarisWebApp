using UnionService.Domain.Entities;

namespace UnionService.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Access> Accesses { get; }
        IRepository<Company> Companies { get; }
        IRepository<PerformerInCompany> Performers { get; }
        IRepository<WorkLog> WorkLogs { get; }
        IRepository<Invitation> Invitations { get; }
        IRepository<Team> Teams { get; }
        IRepository<DateTimeChecker> DateTimeCheckers { get; }

        Task<int> SaveAsync();
    }
}
