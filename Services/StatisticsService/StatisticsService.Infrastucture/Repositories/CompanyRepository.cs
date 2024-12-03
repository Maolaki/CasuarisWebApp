using StatisticsService.Domain.Entities;
using StatisticsService.Infrastructure.Context;

namespace StatisticsService.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company> 
    {
        public CompanyRepository(ApplicationContext applicationContext) : base(applicationContext) {}
    }
}
