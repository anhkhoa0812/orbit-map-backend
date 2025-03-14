using System.ComponentModel.DataAnnotations;

namespace OrbitMap.API.Payload.Request.TravelPlan;

public class CreateTravelPlanDayRequest
{
    [Required] public int Day { get; set; }
    [Required] public List<CreateTravelPlanItemRequest> TravelPlanItems { get; set; }
}