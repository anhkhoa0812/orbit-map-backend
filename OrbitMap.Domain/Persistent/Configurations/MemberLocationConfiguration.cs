using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;

namespace OrbitMap.Domain.Persistent.Configurations;

public class MemberLocationConfiguration : IEntityTypeConfiguration<MemberLocation>
{
    public void Configure(EntityTypeBuilder<MemberLocation> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .HasOne(ml => ml.Location)
            .WithMany(l => l.MemberLocations)
            .HasForeignKey(ml => ml.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(ml => ml.Member)
            .WithMany(m => m.MemberLocations)
            .HasForeignKey(ml => ml.MemberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}