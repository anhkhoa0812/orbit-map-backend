using System.ComponentModel.DataAnnotations;

namespace OrbitMap.API.Payload.Request.Location;

public class UpdateUserLocationRequest
{
    [Required] public double Latitude { get; set; }
    [Required] public double Longitude { get; set; }
}