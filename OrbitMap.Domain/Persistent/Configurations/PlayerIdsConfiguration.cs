using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;

namespace OrbitMap.Domain.Persistent.Configurations;

public class PlayerIdsConfiguration : IEntityTypeConfiguration<PlayerIds>
{
    public void Configure(EntityTypeBuilder<PlayerIds> builder)
    {
        builder.HasKey(x => x.Id);
    }
}