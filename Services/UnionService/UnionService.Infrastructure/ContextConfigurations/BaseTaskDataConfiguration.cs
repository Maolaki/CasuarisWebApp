using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnionService.Domain.Entities;

namespace UnionService.Infrastructure.ContextConfigurations
{
    public class BaseTaskDataConfiguration : IEntityTypeConfiguration<BaseTaskData>
    {
        public void Configure(EntityTypeBuilder<BaseTaskData> builder)
        {
            builder.HasOne(d => d.Info)
                .WithOne(i => i.Data)
                .HasForeignKey<BaseTaskData>(d => d.InfoId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasMany(d => d.Resources)
                .WithOne(r => r.BaseTaskData)
                .HasForeignKey(r => r.BaseTaskDataId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
