namespace OrbitMap.API.Payload.Request.User;

public class UpdateUserRequest
{
    public string? Username { get; set; }
    public string? DisplayName { get; set; }
    public string? ImageBase64 { get; set; }
    public DateOnly? Birthday { get; set; }
}