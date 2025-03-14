using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Payload.Response.TravelPlan;

public class TravelPlanResponse
{
    public Guid Id { get; set; }
    public ETravelPlanType Type { get; set; }
    public List<TravelPlanDayResponse> TravelPlanDays { get; set; }
}