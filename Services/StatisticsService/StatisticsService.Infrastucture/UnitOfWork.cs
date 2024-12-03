using StatisticsService.Domain.Entities;
using StatisticsService.Domain.Interfaces;
using StatisticsService.Infrastructure.Context;
using StatisticsService.Infrastructure.Repositories;

namespace StatisticsService.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private IRepository<User>? userRepository;
        private IRepository<Access>? accessRepository;
        private IRepository<Company>? companyRepository;
        private IRepository<BaseTaskInfo>? taskInfoRepository;
        private IRepository<PerformerInCompany>? performerRepository;
        private IRepository<WorkLog>? workLogRepository;
        private IRepository<Team>? teamRepository;

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

        public IRepository<Company> Companies
        {
            get
            {
                if (companyRepository == null)
                    companyRepository = new CompanyRepository(_context);
                return companyRepository;
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

        public IRepository<PerformerInCompany> Performers
        {
            get
            {
                if (performerRepository == null)
                    performerRepository = new PerformerInCompanyRepository(_context);
                return performerRepository;
            }
        }

        public IRepository<WorkLog> WorkLogs
        {
            get
            {
                if (workLogRepository == null)
                    workLogRepository = new WorkLogRepository(_context);
                return workLogRepository;
            }
        }

        public IRepository<Team> Teams
        {
            get
            {
                if (teamRepository == null)
                    teamRepository = new TeamRepository(_context);
                return teamRepository;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
