namespace OrbitMap.API.Payload.Response.Location;

public class UserLocationDto
{
    public string Username { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Timestamp { get; set; }
}