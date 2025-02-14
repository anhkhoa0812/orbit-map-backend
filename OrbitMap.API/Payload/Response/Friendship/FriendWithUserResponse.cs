using OrbitMap.API.Payload.Response.User;
using OrbitMap.Domain.Enums;

namespace OrbitMap.API.Payload.Response.Friendship;

public class FriendWithUserResponse : UserDto
{
    public string? RequesterUsername { get; set; }
    public string? AddresseeUsername { get; set; }
    public EFriendshipStatus? Status { get; set; }
}