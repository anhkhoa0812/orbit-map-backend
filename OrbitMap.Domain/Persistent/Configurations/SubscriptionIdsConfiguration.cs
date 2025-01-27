using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;

namespace OrbitMap.Domain.Persistent.Configurations;

public class SubscriptionIdsConfiguration : IEntityTypeConfiguration<SubscriptionIds>
{
    public void Configure(EntityTypeBuilder<SubscriptionIds> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .HasOne(u => u.Member)
            .WithMany(m => m.SubscriptionIds)
            .HasForeignKey(u => u.MemberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}