using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Payload.Request.Friendship;

public class UpdateFriendStatusRequest
{
    public string RequestUsername { get; set; }
    public EFriendshipStatus Status { get; set; }
}