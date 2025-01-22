using System.Globalization;
using Humanizer;

namespace OrbitMap.API.Payload.Response.User;

public class UserDto
{
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public DateTime LastActive { get; set; }
    public string? AvatarUrl { get; set; }
    public DateOnly? Birhtday { get; set; }

    public string HumanizeredTime => DateTime.UtcNow.AddHours(LastActive.Hour - DateTime.UtcNow.Hour)
        .Humanize(
            culture: CultureInfo.ReadOnly(CultureInfo.GetCultureInfo("vi-VN"))
        );
}