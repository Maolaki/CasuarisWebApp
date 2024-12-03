using UnionService.Domain.Entities;
using UnionService.Infrastructure.Context;

namespace UnionService.Infrastructure.Repositories
{
    public class PerformerInCompanyRepository : BaseRepository<PerformerInCompany> 
    {
        public PerformerInCompanyRepository(ApplicationContext applicationContext) : base(applicationContext) {}
    }
}
