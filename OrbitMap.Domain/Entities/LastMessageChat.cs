using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace OrbitMap.Domain.Entities;

public class LastMessageChat : EntityBase<Guid>
{
    [Column(TypeName = "jsonb")] public LastMessageChatDocument LastMessageChatDocument { get; set; }
}

public class LastMessageChatDocument
{
    public string SenderUsername { get; set; }
    public string RecipientUsername { get; set; }
    public string Content { get; set; }
    public DateTime MessageLastDate { get; set; }

    public string GroupName { get; set; }

    public bool IsRead { get; set; } = false;
}