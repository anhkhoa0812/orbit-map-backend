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
            Id = Guid.Parse("bc9862b5-6328-44fe-acf9-d5def2c5ea29"),
            BusinessServiceType = EBusinessService.FIRST_RESANDHOTEL,
            Price = 299000
        });
        builder.HasData(new BusinessService()
        {
            Id = Guid.Parse("50cd0e88-e256-424b-b694-bdfe52d40bab"),
            BusinessServiceType = EBusinessService.RESANDHOTEL_1Y,
            Price = 1299000
        });
    }
}