using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskService.Domain.Entities;

namespace TaskService.Infrastructure.ContextConfigurations
{
    public class AccessConfiguration : IEntityTypeConfiguration<Access>
    {
        public void Configure(EntityTypeBuilder<Access> builder)
        {
            builder.HasMany(a => a.Performers)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "AccessPerformers",
                    ap => ap.HasOne<User>()
                            .WithMany()
                            .HasForeignKey("UserId")
                            .OnDelete(DeleteBehavior.Cascade),
                    ap => ap.HasOne<Access>()
                            .WithMany()
                            .HasForeignKey("AccessId")
                            .OnDelete(DeleteBehavior.Cascade)
                );
        }
    }
}
