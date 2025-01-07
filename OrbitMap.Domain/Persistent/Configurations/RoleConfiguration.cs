using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;

namespace OrbitMap.Domain.Persistent.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasData(new Role()
        {
            Id = Guid.Parse("3516c2f0-7f9f-4a5d-9ec0-ee5696c95bb1"),
            Name = "Admin"
        });
        builder.HasData(new Role()
        {
            Id = Guid.Parse("d1cd3eef-3318-48e3-99f7-31a938fbd021"),
            Name = "Member"
        });
    }
}