using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace OrbitMap.Domain.Entities;

public class Message : EntityAuditBase<Guid>
{
    public Guid SenderId { get; set; }
    public string SenderUsername { get; set; }
    public Member Sender { get; set; }
    public Guid RecipientId { get; set; }

    public string RecipientUsername { get; set; }
    public Member Recipient { get; set; }

    public string Content { get; set; }
    public DateTime? DateRead { get; set; }

    public Guid? StoryId { get; set; }

    [ForeignKey(nameof(StoryId))] public Story? Story { get; set; }
}