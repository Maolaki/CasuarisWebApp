using StatisticsService.Domain.Entities;
using StatisticsService.Infrastructure.Context;

namespace StatisticsService.Infrastructure.Repositories
{
    public class PerformerInCompanyRepository : BaseRepository<PerformerInCompany> 
    {
        public PerformerInCompanyRepository(ApplicationContext applicationContext) : base(applicationContext) {}
    }
}
