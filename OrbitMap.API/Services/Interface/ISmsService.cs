using OrbitMap.API.Payload.Request.Sms;

namespace OrbitMap.API.Services.Interface;

public interface ISmsService
{ 
    Task<string> SendOtpAsync(SendOtpRequest request);
}