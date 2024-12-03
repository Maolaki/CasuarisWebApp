using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnionService.Domain.Entities;

namespace UnionService.Infrastructure.ContextConfigurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasMany(t => t.Members)
                .WithMany(u => u.Teams)
                .UsingEntity<Dictionary<string, object>>("TeamsMembers",
                    tm => tm.HasOne<User>()
                          .WithMany()
                          .HasForeignKey("UserId")
                          .OnDelete(DeleteBehavior.Cascade),
                    tm => tm.HasOne<Team>()
                          .WithMany()
                          .HasForeignKey("TeamId")
                          .OnDelete(DeleteBehavior.Cascade)
                );
        }
    }
}
