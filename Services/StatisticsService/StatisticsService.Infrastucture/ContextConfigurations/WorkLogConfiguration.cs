using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StatisticsService.Domain.Entities;

namespace StatisticsService.Infrastructure.ContextConfigurations
{
    public class WorkLogConfiguration : IEntityTypeConfiguration<WorkLog>
    {
        public void Configure(EntityTypeBuilder<WorkLog> builder)
        {
            builder.HasIndex(w => new { w.PerformerInCompanyId, w.WorkDate });
        }
    }
}
