using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Payload.Response.TravelPlan;

public class TravelPlanItemResponse
{
    public string Id { get; set; }
    public ETravelPlanItemTime Time { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string? ImageUrl { get; set; }
}