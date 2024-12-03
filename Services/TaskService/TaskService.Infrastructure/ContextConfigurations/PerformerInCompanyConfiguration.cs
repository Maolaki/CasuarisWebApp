using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskService.Domain.Entities;

namespace TaskService.Infrastructure.ContextConfigurations
{
    public class PerformerInCompanyConfiguration : IEntityTypeConfiguration<PerformerInCompany>
    {
        public void Configure(EntityTypeBuilder<PerformerInCompany> builder)
        {
            builder.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => new { p.CompanyId, p.UserId });
        }
    }
}
