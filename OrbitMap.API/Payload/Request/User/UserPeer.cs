using OrbitMap.API.Payload.Response.User;

namespace OrbitMap.API.Payload.Request.User;

public class UserPeer
{
    public string PeerId { get; set; }
    public UserDto Member { get; set; }
}