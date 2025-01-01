using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace OrbitMap.Domain.Entities;

public class User : EntityAuditBase<Guid>
{
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Username { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string PhoneNumber { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(max)")]
    public string PasswordHash { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? AvatarUrl { get; set; }
    [Column(TypeName = "nvarchar(255)")]
    public string? Bio { get; set; }
    public bool IsPremium { get; set; }
    
    public virtual ICollection<Friendship>? Friendships { get; set; }
}