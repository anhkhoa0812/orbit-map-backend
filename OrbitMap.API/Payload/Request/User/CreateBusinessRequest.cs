namespace OrbitMap.API.Payload.Request.User;

public class CreateBusinessRequest
{
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public IFormFile? AvatarFile { get; set; }
    public string Otp { get; set; }
}