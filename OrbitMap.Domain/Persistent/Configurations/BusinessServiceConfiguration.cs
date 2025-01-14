using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;

namespace OrbitMap.Domain.Persistent.Configurations;

public class BusinessServiceConfiguration : IEntityTypeConfiguration<BusinessService>
{
    public void Configure(EntityTypeBuilder<BusinessService> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(bs => bs.BusinessServiceType)
            .HasConversion(
                v => v.ToString(),
                v => (EBusinessService)Enum.Parse(typeof(EBusinessService), v)
            );
        builder.HasData(new BusinessService()
        {
            Id = Guid.NewGuid(),
            BusinessServiceType = EBusinessService.FIRST_RESANDHOTEL,
            Price = 299000
        });
        builder.HasData(new BusinessService()
        {
            Id = Guid.NewGuid(),
            BusinessServiceType = EBusinessService.RESANDHOTEL_1Y,
            Price = 1299000
        });
    }
}