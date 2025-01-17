namespace OrbitMap.API.Payload.Request.User;

public class UpdateUserRequest
{
    public string? Username { get; set; }
    public string? DisplayName { get; set; }
    public IFormFile? ImageFile { get; set; }
    public DateOnly? Birthday { get; set; }
}