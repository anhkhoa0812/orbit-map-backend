using System.ComponentModel.DataAnnotations;
using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Payload.Request.News;

public class ReactNewsRequest
{
    public EReactionType? ReactionType { get; set; }
}