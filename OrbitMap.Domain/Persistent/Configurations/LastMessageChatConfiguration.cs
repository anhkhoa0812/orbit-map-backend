using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;

namespace OrbitMap.Domain.Persistent.Configurations;

public class LastMessageChatConfiguration : IEntityTypeConfiguration<LastMessageChat>
{
    public void Configure(EntityTypeBuilder<LastMessageChat> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.LastMessageChatDocument).HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<LastMessageChatDocument>(v, (JsonSerializerOptions)null)
        );
    }
}