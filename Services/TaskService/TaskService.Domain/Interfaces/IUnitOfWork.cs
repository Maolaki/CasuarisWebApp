using TaskService.Domain.Entities;

namespace TaskService.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Access> Accesses { get; }
        IRepository<BaseTaskInfo> TasksInfo { get; }
        IRepository<BaseTaskData> TasksData { get; }
        IRepository<Resource> Resources { get; }
        IRepository<Company> Companies { get; }

        Task<int> SaveAsync();
    }
}
