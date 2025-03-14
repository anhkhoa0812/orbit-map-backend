using System.ComponentModel.DataAnnotations;
using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Payload.Request.TravelPlan;

public class CreateTravelPlanItemRequest
{
    [Required] public ETravelPlanItemTime Time { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Address { get; set; }
    public IFormFile? Image { get; set; }
}