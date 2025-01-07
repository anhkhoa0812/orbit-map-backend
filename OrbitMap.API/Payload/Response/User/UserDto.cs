namespace OrbitMap.API.Payload.Response.User;

public class UserDto
{
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public DateTime LastActive { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Bio { get; set; }
}