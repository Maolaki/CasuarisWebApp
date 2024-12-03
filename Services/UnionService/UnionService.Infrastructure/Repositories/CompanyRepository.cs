using UnionService.Domain.Entities;
using UnionService.Infrastructure.Context;

namespace UnionService.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company> 
    {
        public CompanyRepository(ApplicationContext applicationContext) : base(applicationContext) {}
    }
}
