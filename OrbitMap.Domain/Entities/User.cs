using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Utils;

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
    public DateTime LastActive { get; set; } = TimeUtil.GetCurrentSEATime();

    public ICollection<Story>? Stories { get; set; }

    public virtual ICollection<Friendship>? FriendshipRequests { get; set; }
    public virtual ICollection<Friendship>? FriendshipAddressees { get; set; }

    public virtual ICollection<SubscriptionIds>? SubscriptionIds { get; set; } = new List<SubscriptionIds>();

    public virtual ICollection<NewsReaction>? NewsReactions { get; set; }

    public virtual ICollection<Transaction>? Transactions { get; set; }

    public virtual ICollection<MemberLocation>? MemberLocations { get; set; }
}

public class Business : User
{
    public Guid BusinessServiceId { get; set; }

    [ForeignKey(nameof(BusinessServiceId))]
    public BusinessService? BusinessService { get; set; }

    public EBusinessType BusinessType { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public string Address { get; set; }
    public string LocationId { get; set; }
    [ForeignKey(nameof(LocationId))] public Location? Location { get; set; }
}