using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Payload.Response.Business;

public class BusinessResponse
{
    public string DisplayName { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? AvatarUrl { get; set; }
    public EBusinessType BusinessType { get; set; }
}