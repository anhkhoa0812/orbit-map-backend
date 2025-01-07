using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;

namespace OrbitMap.Domain.Persistent.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .HasOne(u => u.Recipient)
            .WithMany(m => m.MessagesReceived)
            .HasForeignKey(u => u.RecipientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(u => u.Sender)
            .WithMany(m => m.MessagesSent)
            .HasForeignKey(u=> u.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}