using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;
using OrbitMap.Domain.Enums;

namespace OrbitMap.Domain.Entities;

public class TravelPlan : EntityBase<Guid>
{
    public ETravelPlanType Type { get; set; }
    public string LocationId { get; set; }

    [ForeignKey(nameof(LocationId))] public Location Location { get; set; }

    public virtual ICollection<TravelPlanDay> TravelPlanDays { get; set; }
}