using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskService.Domain.Entities;

namespace TaskService.Infrastructure.ContextConfigurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasMany(c => c.Owners)
                .WithMany(u => u.Companies)
                .UsingEntity<Dictionary<string, object>>("CompaniesOwners",
                    cm => cm.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade),
                    cm => cm.HasOne<Company>()
                    .WithMany()
                    .HasForeignKey("CompanyId")
                    .OnDelete(DeleteBehavior.Cascade)
            );

            builder.HasMany(c => c.Managers)
                .WithMany(u => u.Companies)
                .UsingEntity<Dictionary<string, object>>("CompaniesManagers",
                    cm => cm.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade),
                    cm => cm.HasOne<Company>()
                    .WithMany()
                    .HasForeignKey("CompanyId")
                    .OnDelete(DeleteBehavior.Cascade)
            );

            builder.HasMany(c => c.Performers)
                .WithOne(p => p.Company)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Accesses)
                .WithOne(a => a.Company)
                .HasForeignKey(a => a.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
