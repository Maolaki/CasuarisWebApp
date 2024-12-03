using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnionService.Domain.Entities;

namespace UnionService.Infrastructure.ContextConfigurations
{
    public class WorkLogConfiguration : IEntityTypeConfiguration<WorkLog>
    {
        public void Configure(EntityTypeBuilder<WorkLog> builder)
        {
            builder.HasIndex(w => new { w.PerformerInCompanyId, w.WorkDate });
        }
    }
}
