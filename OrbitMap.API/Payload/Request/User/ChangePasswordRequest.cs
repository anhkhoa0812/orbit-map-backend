using System.ComponentModel.DataAnnotations;

namespace OrbitMap.API.Payload.Request.User;

public class ChangePasswordRequest
{
    [Required]
    [MinLength(4, ErrorMessage = "Mật khẩu cũ phải có ít nhất 4 ký tự")]
    public string OldPassword { get; set; }

    [Required]
    [MinLength(4, ErrorMessage = "Mật khẩu mới phải có ít nhất 4 ký tự")]
    public string NewPassword { get; set; }
}