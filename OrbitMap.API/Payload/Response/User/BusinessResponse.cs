namespace OrbitMap.API.Payload.Response.User;

public class BusinessResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string PhoneNumber { get; set; }
    public string? AvatarUrl { get; set; }
    public Guid RoleId { get; set; }
    public string Token { get; set; }
}