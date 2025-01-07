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
        builder.HasData(new User()
        {
            Id = Guid.Parse("b1cc911f-7d57-4043-a716-c5249da61270"),
            Username = "admin",
            PasswordHash = Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes("admin"))),
            PhoneNumber = "0123456789",
            CreatedDate = DateTime.Now,
            IsPremium = true,
            RoleId = Guid.Parse("3516c2f0-7f9f-4a5d-9ec0-ee5696c95bb1"),
            LastActive = DateTime.Now,
            DisplayName = "admin",
        });
        builder.HasData(new User()
        {
            Id = Guid.Parse("cacf40b2-772b-4c20-a0c9-cd7359353622"),
            Username = "khoa",
            PasswordHash = Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes("admin"))),
            PhoneNumber = "1234567890",
            CreatedDate = DateTime.Now,
            IsPremium = true,
            RoleId = Guid.Parse("3516c2f0-7f9f-4a5d-9ec0-ee5696c95bb1"),
            LastActive = DateTime.Now,
            DisplayName = "khoa",
        });
        builder.HasData(new User()
        {
            Id = Guid.Parse("68c029f3-b49f-41da-864c-40299f71a956"),
            Username = "hoang",
            PasswordHash = Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes("admin"))),
            PhoneNumber = "0399533724",
            CreatedDate = DateTime.Now,
            IsPremium = true,
            RoleId = Guid.Parse("3516c2f0-7f9f-4a5d-9ec0-ee5696c95bb1"),
            LastActive = DateTime.Now,
            DisplayName = "hoang",
        });
    }
}