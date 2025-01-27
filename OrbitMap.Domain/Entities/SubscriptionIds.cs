using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace OrbitMap.Domain.Entities;

public class SubscriptionIds : EntityBase<Guid>
{
    [Required] public string SubscriptionId { get; set; }
    [Required] public Guid MemberId { get; set; }
    [ForeignKey(nameof(MemberId))] public Member Member { get; set; }
    [Required] public string Username { get; set; }
}