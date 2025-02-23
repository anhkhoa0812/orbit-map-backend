using OrbitMap.API.Payload.Response.Hotel;

namespace OrbitMap.API.Services.Interface;

public interface IOverseaService
{
    Task<List<HotelResponse>> GetNearestHotelFromOversea(double lat, double lng);
}