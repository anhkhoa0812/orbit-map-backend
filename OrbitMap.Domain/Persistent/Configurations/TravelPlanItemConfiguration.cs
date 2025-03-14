using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;

namespace OrbitMap.Domain.Persistent.Configurations;

public class TravelPlanItemConfiguration : IEntityTypeConfiguration<TravelPlanItem>
{
    public void Configure(EntityTypeBuilder<TravelPlanItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(tpi => tpi.Time)
            .HasConversion(
                v => v.ToString(),
                v => (ETravelPlanItemTime)Enum.Parse(typeof(ETravelPlanItemTime), v));
        builder.HasOne(tpi => tpi.TravelPlanDay)
            .WithMany(tpd => tpd.TravelPlanItems)
            .HasForeignKey(tpi => tpi.TravelPlanDayId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}