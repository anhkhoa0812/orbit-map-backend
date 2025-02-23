namespace OrbitMap.API.Services.Interface;

public interface IVietMapService
{
    Task<string?> GetAddressByLocation(double lat, double lng);
}