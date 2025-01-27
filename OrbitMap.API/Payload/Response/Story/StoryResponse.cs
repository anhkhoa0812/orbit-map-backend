using System.Globalization;
using Humanizer;
using Humanizer.Localisation;

namespace OrbitMap.API.Payload.Response.Story;

public class StoryResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string? Location { get; set; }
    public string? Content { get; set; }
    public string MediaUrl { get; set; }
    public string? Weather { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string? AvatarUrl { get; set; }

    public string HumanizedTime => DateTime.UtcNow.AddHours(CreatedDate.Hour - DateTime.UtcNow.Hour).Humanize(
        culture: CultureInfo.ReadOnly(CultureInfo.GetCultureInfo("vi-VN"))
    ).Transform(To.SentenceCase);
    // public string HumanizedTimes => (DateTime.UtcNow - CreatedDate).Humanize(
    //     precision: 1,
    //     culture: System.Globalization.CultureInfo.GetCultureInfo("vi-VN"),
    //     TimeUnit.Day
    // );
}