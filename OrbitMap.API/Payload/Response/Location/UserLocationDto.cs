namespace OrbitMap.API.Payload.Response.Location;

public class UserLocationDto
{
    public string Username { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string DisplayName { get; set; }
    public DateTime LastActive { get; set; }
    public string? AvatarUrl { get; set; }
    public DateOnly? Birhtday { get; set; }
    public DateTime Timestamp { get; set; }
}