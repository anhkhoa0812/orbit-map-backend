using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;

namespace OrbitMap.Domain.Persistent.Configurations;

public class TravelPlanConfiguration : IEntityTypeConfiguration<TravelPlan>
{
    public void Configure(EntityTypeBuilder<TravelPlan> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Location)
            .WithMany(x => x.TravelPlans)
            .HasForeignKey(x => x.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(tp => tp.Type)
            .HasConversion(
                v => v.ToString(),
                v => (ETravelPlanType)Enum.Parse(typeof(ETravelPlanType), v));
    }
}