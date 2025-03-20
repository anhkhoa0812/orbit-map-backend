using OrbitMap.API.Payload.Request.TravelPlan;
using OrbitMap.API.Payload.Response.TravelPlan;
using OrbitMap.Domain.Filter.FilterModel;
using OrbitMap.Domain.Paginate.Interfaces;

namespace OrbitMap.API.Services.Interface;

public interface ITravelPlanService
{
    Task<TravelPlanResponse> CreateTravelPlanAsync(CreateTravelPlanRequest request);

    Task<List<TravelPlanResponse>?> GetTravelPlanAsync(string locationName);

    Task<IPaginate<TravelPlanResponse>?> GetAllTravelPlanPaging(int page, int size, TravelPlanFilter? filter, string?
        sortBy, bool isAsc);

    Task<TravelPlanResponse> GetTravelPlanByIdAsync(Guid id);
}