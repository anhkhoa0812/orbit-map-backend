using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace OrbitMap.Domain.Entities;

public class MemberLocation : EntityBase<Guid>
{
    [Required] public Guid MemberId { get; set; }
    [Required] public string LocationId { get; set; }
    [ForeignKey(nameof(LocationId))] public virtual Location Location { get; set; }
    [ForeignKey(nameof(MemberId))] public virtual Member Member { get; set; }
}