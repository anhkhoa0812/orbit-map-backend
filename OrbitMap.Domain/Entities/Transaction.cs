using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using Contracts.Domains;
using OrbitMap.Domain.Enums;

namespace OrbitMap.Domain.Entities;

public class Transaction : EntityAuditBase<Guid>
{
    [Required]
    [Column(TypeName = "varchar(50)")]
    public long OrderCode { get; set; }

    public Guid MemberId { get; set; }

    [ForeignKey(nameof(MemberId))] public Member Member { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string? Description { get; set; }

    [Required] public ETransactionStatus Status { get; set; } = ETransactionStatus.Pending;
}