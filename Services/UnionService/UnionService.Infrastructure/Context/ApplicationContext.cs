using Microsoft.EntityFrameworkCore;
using UnionService.Domain.Entities;

namespace UnionService.Infrastructure.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<BaseTaskInfo> BaseTasksInfo { get; set; } = null!;
        public DbSet<BaseTaskData> BaseTasksData { get; set; } = null!;
        public DbSet<Resource> Resources { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<PerformerInCompany> Performers { get; set; } = null!;
        public DbSet<WorkLog> WorkLogs { get; set; } = null!;
        public DbSet<DateTimeChecker> DateTimeCheckers { get; set; } = null!;
        public DbSet<Invitation> Invitations { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}
