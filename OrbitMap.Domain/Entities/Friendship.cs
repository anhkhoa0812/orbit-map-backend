using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;
using OrbitMap.Domain.Enums;

namespace OrbitMap.Domain.Entities;

public class Friendship : EntityAuditBase<Guid>
{
    public Guid RequesterId { get; set; }
    public Guid AddresseeId { get; set; }
    
    public EFriendshipStatus Status { get; set; } // Pending, Accepted, Rejected - Change to ENUM

    [ForeignKey(nameof(RequesterId))]
    public User Requester { get; set; } = null!;
    [ForeignKey(nameof(AddresseeId))]
    public User Addressee { get; set; } = null!;
}