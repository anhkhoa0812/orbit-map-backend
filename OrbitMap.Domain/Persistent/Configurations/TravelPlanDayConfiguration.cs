using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;

namespace OrbitMap.Domain.Persistent.Configurations;

public class TravelPlanDayConfiguration : IEntityTypeConfiguration<TravelPlanDay>
{
    public void Configure(EntityTypeBuilder<TravelPlanDay> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(tpd => tpd.TravelPlan)
            .WithMany(tp => tp.TravelPlanDays)
            .HasForeignKey(tpd => tpd.TravelPlanId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}