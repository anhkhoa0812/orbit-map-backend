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
}