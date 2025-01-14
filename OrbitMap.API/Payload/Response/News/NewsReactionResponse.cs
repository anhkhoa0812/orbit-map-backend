using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Payload.Response.News;

public class NewsReactionResponse
{
    public string Username { get; set; }

    public Guid NewsId { get; set; }

    public EReactionType ReactionType { get; set; }
}