using System.ComponentModel.DataAnnotations;

namespace OrbitMap.API.Payload.Request.User;

public class RegisterRequest
{
    [Required]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters long")]
    [MaxLength(50, ErrorMessage = "Username must be at most 50 characters long")]
    public string Username { get; set; }
    [Required]
    [MaxLength(255, ErrorMessage = "Display name must be at most 255 characters long")]
    public string DisplayName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; }
    public string Otp { get; set; }
}