using System.ComponentModel.DataAnnotations;

namespace OrbitMap.API.Payload.Response.Device;

public class DeviceInfo
{
    [Required] public string SubscriptionId { get; set; }
    [Required] public string ConnectionId { get; set; }
}