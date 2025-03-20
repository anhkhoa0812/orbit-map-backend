using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Payload.Request.News;

public class UpdateNewsRequest
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? BusinessName { get; set; }
    public string? BusinessAddress { get; set; }
    public IFormFile? BusinessImageFile { get; set; }
    public IFormFile? BannerImageFile { get; set; }

    public List<IFormFile>? NewsImageFiles { get; set; }
    public int? UsefulReactionCount { get; set; }
    public int? UselessReactionCount { get; set; }
    public ENewsType? Type { get; set; }
    public DateTime? ExpirationDate { get; set; }
}