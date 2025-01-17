namespace OrbitMap.API.Payload.Response.User;

public class MemberDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string PhoneNumber { get; set; }
    public string? AvatarUrl { get; set; }
    public bool isPremium { get; set; }
    public DateTime? ExpiredRankDate { get; set; }

    public ICollection<UserDto> Friends { get; set; }
}