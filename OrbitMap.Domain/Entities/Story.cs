using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace OrbitMap.Domain.Entities;

public class Story : EntityAuditBase<Guid>
{
    [Column(TypeName = "varchar(255)")] public string? Content { get; set; }

    [Required]
    [Column(TypeName = "varchar")]
    public string MediaUrl { get; set; }

    [Column(TypeName = "varchar(50)")] public string? Location { get; set; }

    [Column(TypeName = "varchar(50)")] public string? Weather { get; set; }

    [Required] public DateTime ExpirationDate { get; set; }

    [Required] public bool IsDisabled { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))] public virtual Member Member { get; set; } = null!;
}