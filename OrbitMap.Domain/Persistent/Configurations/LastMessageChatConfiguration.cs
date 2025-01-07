using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;

namespace OrbitMap.Domain.Persistent.Configurations;

public class LastMessageChatConfiguration : IEntityTypeConfiguration<LastMessageChat>
{
    public void Configure(EntityTypeBuilder<LastMessageChat> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .HasOne(u => u.Recipient)
            .WithMany(m => m.LastMessageChatsReceived)
            .HasForeignKey(u => u.RecipientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(u => u.Sender)
            .WithMany(m => m.LastMessageChatsSent)
            .HasForeignKey(u => u.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}