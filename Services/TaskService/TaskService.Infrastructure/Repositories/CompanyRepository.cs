using TaskService.Domain.Entities;
using TaskService.Infrastructure.Context;

namespace TaskService.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company> 
    {
        public CompanyRepository(ApplicationContext applicationContext) : base(applicationContext) {}
    }
}
