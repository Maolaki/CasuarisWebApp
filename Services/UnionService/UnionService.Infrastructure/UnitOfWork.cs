using UnionService.Domain.Entities;
using UnionService.Domain.Interfaces;
using UnionService.Infrastructure.Context;
using UnionService.Infrastructure.Repositories;

namespace UnionService.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private IRepository<User>? userRepository;
        private IRepository<Access>? accessRepository;
        private IRepository<Company>? companyRepository;
        private IRepository<PerformerInCompany>? performerRepository;
        private IRepository<WorkLog>? workLogRepository;
        private IRepository<Invitation>? invitationRepository;
        private IRepository<Team>? teamRepository;
        private IRepository<DateTimeChecker>? dateTimeCheckerRepository;

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

        public IRepository<Invitation> Invitations
        {
            get
            {
                if (invitationRepository == null)
                    invitationRepository = new InvitationRepository(_context);
                return invitationRepository;
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

        public IRepository<DateTimeChecker> DateTimeCheckers
        {
            get
            {
                if (dateTimeCheckerRepository == null)
                    dateTimeCheckerRepository = new DateTimeCheckerRepository(_context);
                return dateTimeCheckerRepository;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
