using System.ComponentModel.DataAnnotations;

namespace OrbitMap.API.Payload.Request.Story;

public class CreateStoryRequest
{
    public string? Location { get; set; }
    public string? Content { get; set; }
    [Required] public IFormFile ImageFile { get; set; }
    public string? Weather { get; set; }

    public string? Time { get; set; }

    [Required] public string CityLocation { get; set; }
}