using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;
using OrbitMap.Domain.Enums;

namespace OrbitMap.Domain.Entities;

public class NewsReaction : EntityAuditBase<Guid>
{
    public string Username { get; set; }
    public Guid NewsId { get; set; }
    [ForeignKey(nameof(NewsId))] public News News { get; set; } = null!;
    public Guid MemberId { get; set; }
    [ForeignKey(nameof(MemberId))] public Member Member { get; set; } = null!;
    public EReactionType ReactionType { get; set; }
}