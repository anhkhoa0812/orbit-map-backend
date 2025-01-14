using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Payload.Response.News;

public class NewsByTypeResponse
{
    public ENewsType Type { get; set; }
    public ICollection<NewsWithReactionResponse>? News { get; set; }
}