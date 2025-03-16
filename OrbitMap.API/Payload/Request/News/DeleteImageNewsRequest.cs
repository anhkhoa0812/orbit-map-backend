using System.ComponentModel.DataAnnotations;

namespace OrbitMap.API.Payload.Request.News;

public class DeleteImageNewsRequest
{
    [Required] public string ImageUrl { get; set; }
}