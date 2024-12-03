using UnionService.Domain.Entities;
using UnionService.Infrastructure.Context;

namespace UnionService.Infrastructure.Repositories
{
    public class DateTimeCheckerRepository : BaseRepository<DateTimeChecker>
    {
        public DateTimeCheckerRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    }
}
