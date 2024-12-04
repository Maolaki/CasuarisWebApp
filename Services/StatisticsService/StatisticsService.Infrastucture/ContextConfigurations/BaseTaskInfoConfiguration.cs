using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StatisticsService.Domain.Entities;

namespace StatisticsService.Infrastructure.ContextConfigurations
{
    public class BaseTaskInfoConfiguration : IEntityTypeConfiguration<BaseTaskInfo>
    {
        public void Configure(EntityTypeBuilder<BaseTaskInfo> builder)
        {
            builder.HasOne(t => t.Company)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CompanyId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(t => t.ParentTask)
                .WithMany(t => t.ChildTasks)
                .HasForeignKey(t => t.ParentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
