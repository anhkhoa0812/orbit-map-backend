using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitMap.Domain.Entities;

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
            Id = Guid.NewGuid(),
            Username = "admin",
            PasswordHash = Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes("admin"))),
            PhoneNumber = "0123456789",
            CreatedDate = DateTimeOffset.Now,
            IsPremium = true,
        });
    }
}