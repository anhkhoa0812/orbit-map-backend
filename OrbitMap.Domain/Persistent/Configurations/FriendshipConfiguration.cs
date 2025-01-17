using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;

namespace OrbitMap.Domain.Persistent.Configurations;

public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
{
    public void Configure(EntityTypeBuilder<Friendship> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Status)
            .HasConversion(
                v => v.ToString(),
                v => (EFriendshipStatus)Enum.Parse(typeof(EFriendshipStatus), v)
            );
        builder.HasOne(f => f.Requester)
            .WithMany(u => u.FriendshipRequests)
            .HasForeignKey(f => f.RequesterId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(f => f.Addressee)
            .WithMany(u => u.FriendshipAddressees)
            .HasForeignKey(f => f.AddresseeId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasData(new Friendship()
        {
            Id = Guid.Parse("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
            RequesterId = Guid.Parse("b1cc911f-7d57-4043-a716-c5249da61270"),
            AddresseeId = Guid.Parse("cacf40b2-772b-4c20-a0c9-cd7359353622"),
            CreatedDate = DateTime.UtcNow,
            Status = EFriendshipStatus.Accepted
        });
    }
}