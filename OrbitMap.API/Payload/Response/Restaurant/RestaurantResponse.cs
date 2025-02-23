using System.Text.Json.Serialization;

namespace OrbitMap.API.Payload.Response.Restaurant;

public class RestaurantDataDto
{
    [JsonPropertyName("Total")] public int Total { get; set; }
    [JsonPropertyName("Count")] public int Count { get; set; }
    [JsonPropertyName("NextId")] public int NextId { get; set; }
    [JsonPropertyName("Items")] public List<RestaurantItemDto> Items { get; set; }
}

public class RestaurantItemDto
{
    [JsonPropertyName("Name")] public string Name { get; set; }
    [JsonPropertyName("Avatar")] public string Avatar { get; set; }
    [JsonPropertyName("Address")] public string Address { get; set; }
}

public class CategoryDto
{
    [JsonPropertyName("Id")] public int Id { get; set; }
    [JsonPropertyName("Name")] public string Name { get; set; }
}