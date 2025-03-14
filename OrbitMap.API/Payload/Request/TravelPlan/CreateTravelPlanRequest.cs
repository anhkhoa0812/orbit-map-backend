using System.ComponentModel.DataAnnotations;
using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Payload.Request.TravelPlan;

public class CreateTravelPlanRequest
{
    [Required] public ETravelPlanType Type { get; set; }

    [Required] public string LocationName { get; set; }

    [Required] public List<CreateTravelPlanDayRequest> TravelPlanDays { get; set; }
}