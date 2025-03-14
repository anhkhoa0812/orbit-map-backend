using System.Globalization;
using Humanizer;
using OrbitMap.API.Utils;
using OrbitMap.Domain.Utils;

namespace OrbitMap.API.Payload.Response.User;

public class UserDto
{
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public DateTime LastActive { get; set; }
    public string? AvatarUrl { get; set; }
    public DateOnly? Birhtday { get; set; }

    public string HumanizedTime => TimeUtil.GetCurrentSEATime().Add(LastActive - TimeUtil.GetCurrentSEATime())
        .Humanize(
            culture: CultureInfo.ReadOnly(CultureInfo.GetCultureInfo("vi-VN"))
        );
}