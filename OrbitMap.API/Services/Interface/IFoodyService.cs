using OrbitMap.API.Payload.Response.Restaurant;

namespace OrbitMap.API.Services.Interface;

public interface IFoodyService
{
    Task<List<RestaurantItemDto>> GetNearestRestaurant(double latitude, double longitude);
}