using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;

namespace OrbitMap.Domain.Persistent.Configurations;

public class StoryConfiguration : IEntityTypeConfiguration<Story>
{
    public void Configure(EntityTypeBuilder<Story> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .HasOne(u => u.User)
            .WithMany(m => m.Stories)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}