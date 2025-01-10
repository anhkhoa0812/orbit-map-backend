using System.Collections;
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
    [Column(TypeName = "nvarchar(255)")]
    public string DisplayName { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string PhoneNumber { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(max)")]
    public string PasswordHash { get; set; }

    [Column(TypeName = "nvarchar(max)")] 
    public string? AvatarUrl { get; set; }
    [Column(TypeName = "date")] 
    public DateOnly? Birthday { get; set; }
    public bool IsPremium { get; set; }

    public DateTime LastActive { get; set; } = DateTime.Now;

    public ICollection<Message> MessagesSent { get; set; }

    public ICollection<Message> MessagesReceived { get; set; }

    public ICollection<LastMessageChat> LastMessageChatsSent { get; set; }

    public ICollection<LastMessageChat> LastMessageChatsReceived { get; set; }
    
    public ICollection<Story> Stories { get; set; }
    public Guid RoleId { get; set; }
    [ForeignKey(nameof(RoleId))] public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Friendship>? FriendshipRequests { get; set; }
    public virtual ICollection<Friendship>? FriendshipAddressees { get; set; }
    
    public virtual ICollection<PlayerIds> PlayerIds { get; set; } = new List<PlayerIds>();
}