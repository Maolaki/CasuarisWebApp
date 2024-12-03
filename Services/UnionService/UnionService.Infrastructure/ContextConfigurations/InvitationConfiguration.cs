using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnionService.Domain.Entities;

namespace UnionService.Infrastructure.ContextConfigurations
{
    public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(n => n.Company)
                .WithMany()
                .HasForeignKey(n => n.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(n => n.Team)
                .WithMany()
                .HasForeignKey(n => n.TeamId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
