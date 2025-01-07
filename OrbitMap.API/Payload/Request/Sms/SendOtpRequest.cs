using System.ComponentModel.DataAnnotations;

namespace OrbitMap.API.Payload.Request.Sms;

public class SendOtpRequest
{
    [Required]
    public string PhoneNumber { get; set; }
}