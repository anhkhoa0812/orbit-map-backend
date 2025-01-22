using System.ComponentModel.DataAnnotations;

namespace OrbitMap.API.Payload.Request.User;

public class ForgetPasswordRequest
{
    [Required] public string PhoneNumber { get; set; }
    [Required] public string Otp { get; set; }

    [Required]
    [MinLength(4, ErrorMessage = "Mật khẩu mới phải có ít nhất 4 ký tự")]
    public string NewPassword { get; set; }
}