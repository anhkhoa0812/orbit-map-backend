using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace OrbitMap.Domain.Entities;

public class TravelPlanDay : EntityBase<Guid>
{
    [Required] public int Day { get; set; }
    [Required] public Guid TravelPlanId { get; set; }
    [ForeignKey(nameof(TravelPlanId))] public TravelPlan TravelPlan { get; set; }

    public virtual ICollection<TravelPlanItem> TravelPlanItems { get; set; }
}