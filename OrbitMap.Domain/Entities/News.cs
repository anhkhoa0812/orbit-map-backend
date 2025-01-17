using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;
using OrbitMap.Domain.Enums;

namespace OrbitMap.Domain.Entities;

public class News : EntityAuditBase<Guid>
{
    [Required]
    [Column(TypeName = "varchar(500)")]
    public string Title { get; set; }

    [Required]
    [Column(TypeName = "varchar")]
    public string Content { get; set; }

    public List<string>? ImageUrls { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string BusinessName { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string BusinessAddress { get; set; }

    [Required] public string BusinessImage { get; set; }

    public string? BannerImage { get; set; }
    [Required] public int UsefulReactionCount { get; set; }
    [Required] public int UselessReactionCount { get; set; }
    [Required] public ENewsType Type { get; set; }
    [Required] public DateTime ExpirationDate { get; set; }
    public virtual ICollection<NewsReaction> NewsReactions { get; set; }
}