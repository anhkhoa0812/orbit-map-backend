using System.Text.Json.Serialization;

namespace OrbitMap.API.Payload.Response.VietMap;

public class VietMapResponse
{
    [JsonPropertyName("lat")] public double Lat { get; set; }

    [JsonPropertyName("lng")] public double Lng { get; set; }

    [JsonPropertyName("ref_id")] public string RefId { get; set; }

    [JsonPropertyName("distance")] public double Distance { get; set; }

    [JsonPropertyName("address")] public string Address { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("display")] public string Display { get; set; }
    [JsonPropertyName("boundaries")] public List<Boundaries> Boundaries { get; set; }
    [JsonPropertyName("categories")] public List<object> Categories { get; set; }
}

public class Boundaries
{
    [JsonPropertyName("type")] public int Type { get; set; }

    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("prefix")] public string Prefix { get; set; }

    [JsonPropertyName("full_name")] public string FullName { get; set; }
}