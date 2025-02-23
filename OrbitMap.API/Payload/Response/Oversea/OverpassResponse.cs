using System.Text.Json.Serialization;

namespace OrbitMap.API.Payload.Response.Oversea;

public class OverpassResponse
{
    [JsonPropertyName("version")] public double Version { get; set; }
    [JsonPropertyName("generator")] public string Generator { get; set; }
    [JsonPropertyName("osm3s")] public Osm3s Osm3S { get; set; }
    [JsonPropertyName("elements")] public List<Element> Elements { get; set; }
}

public class Element
{
    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("lat")] public double Lat { get; set; }

    [JsonPropertyName("lon")] public double Lon { get; set; }

    [JsonPropertyName("tags")] public Dictionary<string, string> Tags { get; set; }
}

public class Osm3s
{
    [JsonPropertyName("timestamp_osm_base")]
    public DateTime TimeStampOsmBase { get; set; }

    [JsonPropertyName("copyright")] public string CopyRight { get; set; }
}