using StatisticsService.Domain.Entities;

namespace StatisticsService.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Access> Accesses { get; }
        IRepository<Company> Companies { get; }
        IRepository<BaseTaskInfo> TasksInfo { get; }
        IRepository<PerformerInCompany> Performers { get; }
        IRepository<WorkLog> WorkLogs { get; }
        IRepository<Team> Teams { get; }

        Task<int> SaveAsync();
    }
}
