using System.ComponentModel.DataAnnotations;

namespace OrbitMap.API.Payload.Request.Story;

public class CreateStoryRequest
{
    public string? Location { get; set; }
    public string? Content { get; set; }
    [Required]
    public string ImageBase64 { get; set; }
    public string? Weather { get; set; }
}