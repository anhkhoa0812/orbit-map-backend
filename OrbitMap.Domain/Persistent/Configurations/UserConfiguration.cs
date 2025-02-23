using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;

namespace OrbitMap.Domain.Persistent.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Username).IsUnique();
        builder.HasIndex(x => x.PhoneNumber).IsUnique();
        builder.HasData(new User
        {
            Id = Guid.Parse("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
            Username = "admin",
            PasswordHash = Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes("admin"))),
            PhoneNumber = "8123456789",
            CreatedDate = DateTime.UtcNow,
            RoleId = Guid.Parse("3516c2f0-7f9f-4a5d-9ec0-ee5696c95bb1"),
            DisplayName = "admin",
            AvatarUrl =
                "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736499707/ad62b614-4cb7-4e59-af56-ebd47319cf6b.jpg"
        });
    }
}

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.Property(x => x.Birthday)
            .HasColumnType("date");
        builder.HasData(new Member
        {
            Id = Guid.Parse("b1cc911f-7d57-4043-a716-c5249da61270"),
            Username = "khoa",
            PasswordHash = Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes("khoa"))),
            PhoneNumber = "0123456789",
            CreatedDate = DateTime.UtcNow,
            RoleId = Guid.Parse("d1cd3eef-3318-48e3-99f7-31a938fbd021"),
            DisplayName = "Khoa Gió Tai",
            Birthday = DateOnly.Parse("1999-01-01"),
            IsPremium = true,
            AvatarUrl =
                "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736499707/ad62b614-4cb7-4e59-af56-ebd47319cf6b.jpg"
        });
        builder.HasData(new Member
        {
            Id = Guid.Parse("cacf40b2-772b-4c20-a0c9-cd7359353622"),
            Username = "hoang",
            PasswordHash = Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes("hoang"))),
            PhoneNumber = "1234567890",
            CreatedDate = DateTime.UtcNow,
            RoleId = Guid.Parse("d1cd3eef-3318-48e3-99f7-31a938fbd021"),
            DisplayName = "Hoàng Gió Nhải",
            Birthday = DateOnly.Parse("1999-01-01"),
            IsPremium = true,
            AvatarUrl =
                "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736250368/fc72a64e-91fd-495b-bf19-b0f9ae97f1cc.png"
        });
        builder.HasData(new Member
        {
            Id = Guid.Parse("68c029f3-b49f-41da-864c-40299f71a956"),
            Username = "quan",
            PasswordHash = Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes("quan"))),
            PhoneNumber = "0399533724",
            CreatedDate = DateTime.UtcNow,
            RoleId = Guid.Parse("d1cd3eef-3318-48e3-99f7-31a938fbd021"),
            DisplayName = "quan",
            Birthday = DateOnly.Parse("1999-01-01"),
            IsPremium = true,
            AvatarUrl =
                "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736250368/fc72a64e-91fd-495b-bf19-b0f9ae97f1cc.png"
        });
    }
}

public class BusinessConfiguration : IEntityTypeConfiguration<Business>
{
    public void Configure(EntityTypeBuilder<Business> builder)
    {
        builder.Property(bs => bs.BusinessType)
            .HasConversion(
                v => v.ToString(),
                v => (EBusinessType)Enum.Parse(typeof(EBusinessType), v)
            );
        builder
            .HasOne(b => b.Location)
            .WithMany(m => m.Businesses)
            .HasForeignKey(ml => ml.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasData(new Business
        {
            Id = Guid.Parse("edda5af1-27b1-466b-9037-ab4a91b269d8"),
            Username = "pizza4p",
            PasswordHash = Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes("pizza4p"))),
            PhoneNumber = "0435377485",
            CreatedDate = DateTime.UtcNow,
            RoleId = Guid.Parse("3fd223f6-3edd-4c87-888a-35defcff39e8"),
            DisplayName = "PIZZA 4P'S",
            AvatarUrl =
                "https://s3-hcm5-r1.longvan.net/19429498-orbitmap/10000010_2.jpg",
            BusinessType = EBusinessType.Restaurant,
            BusinessServiceId = Guid.Parse("50cd0e88-e256-424b-b694-bdfe52d40bab"),
            Latitude = 10.8018374,
            LocationId = "vnSG",
            Longitude = 106.745865,
            Address = "8/15 Lê Thánh Tôn, Bến Nghé, Quận 1, Thành phố Hồ Chí Minh"
        });
    }
}