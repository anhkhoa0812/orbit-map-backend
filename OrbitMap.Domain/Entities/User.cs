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
    [Column(TypeName = "varchar(255)")]
    public string DisplayName { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string PhoneNumber { get; set; }

    [Required]
    [Column(TypeName = "varchar")]
    public string PasswordHash { get; set; }

    [Column(TypeName = "varchar")] public string? AvatarUrl { get; set; }

    public Guid RoleId { get; set; }

    [ForeignKey(nameof(RoleId))] public virtual Role Role { get; set; } = null!;
}

public class Member : User
{
    [Column(TypeName = "date")] public DateOnly? Birthday { get; set; }

    public bool IsPremium { get; set; }

    public DateTime? ExpiredRankDate { get; set; }
    public DateTime LastActive { get; set; } = DateTime.UtcNow;

    public ICollection<Story> Stories { get; set; }

    public virtual ICollection<Friendship>? FriendshipRequests { get; set; }
    public virtual ICollection<Friendship>? FriendshipAddressees { get; set; }

    public virtual ICollection<PlayerIds> PlayerIds { get; set; } = new List<PlayerIds>();

    public virtual ICollection<NewsReaction> NewsReactions { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; }
}

public class Business : User
{
    public Guid BusinessServiceId { get; set; }

    [ForeignKey(nameof(BusinessServiceId))]
    public BusinessService? BusinessService { get; set; }
}