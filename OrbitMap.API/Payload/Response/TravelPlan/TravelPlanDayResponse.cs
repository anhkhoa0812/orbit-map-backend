namespace OrbitMap.API.Payload.Response.TravelPlan;

public class TravelPlanDayResponse
{
    public Guid Id { get; set; }
    public int Day { get; set; }
    public List<TravelPlanItemResponse> TravelPlanItems { get; set; }
}