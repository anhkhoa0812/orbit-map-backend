using System.ComponentModel.DataAnnotations;

namespace OrbitMap.API.Payload.Request.Sms;

public class SendOtpRequest
{
    [Required] public string? Username { get; set; }
    [Required] public string PhoneNumber { get; set; }
    [Required] public bool IsRegister { get; set; }
}