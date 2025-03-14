using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;
using OrbitMap.Domain.Enums;

namespace OrbitMap.Domain.Entities;

public class TravelPlanItem : EntityBase<Guid>
{
    [Required] public ETravelPlanItemTime Time { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Address { get; set; }
    public string? ImageUrl { get; set; }

    public Guid TravelPlanDayId { get; set; }
    [ForeignKey(nameof(TravelPlanDayId))] public TravelPlanDay TravelPlanDay { get; set; }
}