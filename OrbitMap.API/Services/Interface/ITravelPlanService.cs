using OrbitMap.API.Payload.Request.TravelPlan;
using OrbitMap.API.Payload.Response.TravelPlan;

namespace OrbitMap.API.Services.Interface;

public interface ITravelPlanService
{
    Task<TravelPlanResponse> CreateTravelPlanAsync(CreateTravelPlanRequest request);

    Task<List<TravelPlanResponse>?> GetTravelPlanAsync(string locationName);
}