using System.ComponentModel.DataAnnotations;

namespace OrbitMap.API.Payload.Request.User;

public class LoginRequest
{
    [Required(ErrorMessage = "Username or PhoneNumber is required")]
    [MaxLength(50, ErrorMessage = "Username or PhoneNumber's max length is 50 characters")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [MinLength(5, ErrorMessage = "Password's min length is 5 characters")]
    [MaxLength(64, ErrorMessage = "Password's max length is 64 characters")]
    public string Password { get; set; }
}