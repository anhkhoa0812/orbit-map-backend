using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace OrbitMap.Domain.Entities;

public class Message : EntityBase<Guid>
{
    [Column(TypeName = "jsonb")] public MessageDocument MessageDocument { get; set; }
}

public class MessageDocument
{
    public string SenderUsername { get; set; }
    public string RecipientUsername { get; set; }
    public string Content { get; set; }
    public DateTime? DateRead { get; set; }
    public Guid? StoryId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}