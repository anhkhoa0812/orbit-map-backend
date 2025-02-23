using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.Business;
using OrbitMap.API.Payload.Response.Hotel;
using OrbitMap.API.Payload.Response.Restaurant;

namespace OrbitMap.API.Services.Interface;

public interface IBusinessService
{
    public Task<CreateBusinessResponse> CreateBusinessAsync(CreateBusinessRequest request);
    Task<List<RestaurantItemDto>> GetNearestRestaurant(double latitude, double longitude, string location);

    Task<List<HotelResponse>> GetNearestHotel(double latitude, double longitude, string location);

    Task<List<BusinessResponse>> GetBusinessesAsync();
}