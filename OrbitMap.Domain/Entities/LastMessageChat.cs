using Contracts.Domains;

namespace OrbitMap.Domain.Entities;

public class LastMessageChat : EntityBase<Guid>
{
    public Guid SenderId { get; set; }

    public string SenderUsername { get; set; }

    public Member Sender { get; set; }

    public Guid RecipientId { get; set; }

    public string RecipientUsername { get; set; }

    public Member Recipient { get; set; }

    public string Content { get; set; }

    public DateTime MessageLastDate { get; set; }

    public string GroupName { get; set; }

    public bool IsRead { get; set; } = false;
}