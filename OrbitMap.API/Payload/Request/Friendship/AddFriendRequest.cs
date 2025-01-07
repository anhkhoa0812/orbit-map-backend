using System.ComponentModel.DataAnnotations;

namespace OrbitMap.API.Payload.Request.Friendship;

public class AddFriendRequest
{
    [Required]
    [MinLength(1, ErrorMessage = "Username must be at least 1 character long")]
    public string Username { get; set; }
}