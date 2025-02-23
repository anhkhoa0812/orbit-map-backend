using System.ComponentModel.DataAnnotations;

namespace OrbitMap.API.Payload.Request.User;

public class DeleteUserRequest
{
    [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
    public string Password { get; set; }
}