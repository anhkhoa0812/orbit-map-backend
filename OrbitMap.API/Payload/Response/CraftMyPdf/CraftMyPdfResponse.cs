using System.Text.Json.Serialization;

namespace OrbitMap.API.Payload.Response.CraftMyPdf;

public class CraftMyPdfResponse
{
    [JsonPropertyName("file")] public string File { get; set; }

    [JsonPropertyName("transaction_ref")] public string TransactionRef { get; set; }

    [JsonPropertyName("status")] public string Status { get; set; }
}