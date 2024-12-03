using UnionService.Domain.Entities;
using UnionService.Infrastructure.Context;

namespace UnionService.Infrastructure.Repositories
{
    public class InvitationRepository : BaseRepository<Invitation>
    {
        public InvitationRepository(ApplicationContext applicationContext) : base(applicationContext) { }
    }
}
