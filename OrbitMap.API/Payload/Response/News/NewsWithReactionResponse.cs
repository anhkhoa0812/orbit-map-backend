using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Payload.Response.News;

public class NewsWithReactionResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public ICollection<string>? ImageUrls { get; set; }
    public string BusinessName { get; set; }
    public string BusinessAddress { get; set; }
    public string BusinessImage { get; set; }
    public string BannerImage { get; set; }
    public int UsefulReactionCount { get; set; }
    public int UselessReactionCount { get; set; }
    public ENewsType Type { get; set; }
    public bool IsUseful { get; set; }
    public bool IsUseless { get; set; }
}