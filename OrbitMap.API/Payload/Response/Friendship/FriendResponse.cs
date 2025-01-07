using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Payload.Response.Friendship;

public class FriendResponse
{
    public Guid RequesterId { get; set; }
    public Guid AddresseeId { get; set; }
    public EFriendshipStatus Status { get; set; }
}