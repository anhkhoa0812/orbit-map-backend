using OrbitMap.API.Utils;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Utils;

namespace OrbitMap.API.Payload.Response.User;

public class LoginResponse
{
    public string Token { get; set; }
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string PhoneNumber { get; set; }
    public string? AvatarUrl { get; set; }
    public DateOnly? Birthday { get; set; }
    public bool IsPremium { get; set; }
    public DateTime LastActive { get; set; } = TimeUtil.GetCurrentSEATime();
    public string Role { get; set; }
}