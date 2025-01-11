using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace OrbitMap.Domain.Entities;

public class Story : EntityAuditBase<Guid>
{
    [Column(TypeName = "nvarchar(255)")] public string? Content { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(max)")]
    public string MediaUrl { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Location { get; set; } //Cần xem lại tách ra 1 bảng riêng

    [Column(TypeName = "nvarchar(50)")] public string? Weather { get; set; }

    [Required] public DateTime ExpirationDate { get; set; }

    [Required] public bool IsDisabled { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))] public virtual Member Member { get; set; } = null!;
}