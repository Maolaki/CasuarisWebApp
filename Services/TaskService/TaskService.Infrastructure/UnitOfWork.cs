using TaskService.Domain.Entities;
using TaskService.Domain.Interfaces;
using TaskService.Infrastructure.Context;
using TaskService.Infrastructure.Repositories;

namespace TaskService.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private IRepository<User>? userRepository;
        private IRepository<Access>? accessRepository;
        private IRepository<BaseTaskInfo>? taskInfoRepository;
        private IRepository<BaseTaskData>? taskDataRepository;
        private IRepository<Resource>? resourceRepository;
        private IRepository<Company>? companyRepository;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(_context);
                return userRepository;
            }
        }

        public IRepository<Access> Accesses
        {
            get
            {
                if (accessRepository == null)
                    accessRepository = new AccessRepository(_context);
                return accessRepository;
            }
        }

        public IRepository<BaseTaskInfo> TasksInfo
        {
            get
            {
                if (taskInfoRepository == null)
                    taskInfoRepository = new BaseTaskInfoRepository(_context);
                return taskInfoRepository;
            }
        }

        public IRepository<BaseTaskData> TasksData
        {
            get
            {
                if (taskDataRepository == null)
                    taskDataRepository = new BaseTaskDataRepository(_context);
                return taskDataRepository;
            }
        }

        public IRepository<Resource> Resources
        {
            get
            {
                if (resourceRepository == null)
                    resourceRepository = new ResourceRepository(_context);
                return resourceRepository;
            }
        }

        public IRepository<Company> Companies
        {
            get
            {
                if (companyRepository == null)
                    companyRepository = new CompanyRepository(_context);
                return companyRepository;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
