using System.Text.Json;
using AutoMapper;
using OrbitMap.API.Payload.Request.Sms;
using OrbitMap.API.Payload.Response.Sms;
using OrbitMap.API.Services.Interface;
using OrbitMap.API.Utils;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class SmsService : BaseService<SmsService>, ISmsService
{
    private readonly IConfiguration _configuration;
    private readonly IRedisService _redisService;
    public SmsService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IRedisService redisService) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
        _configuration = configuration;
        _redisService = redisService;
    }

    public async Task<string> SendOtpAsync(SendOtpRequest request)
    {
        var key = request.PhoneNumber;

        var existingOtp = await _redisService.GetStringAsync(key);

        if (!string.IsNullOrEmpty(existingOtp))
            throw new BadHttpRequestException("Mã OTP đã được gửi");

        var phoneNumberArray = new string[] { request.PhoneNumber };
        var otp = SmsUtil.GenerateOtp();
        var content = "Mã OTP của bạn là: " + otp;
        var response = SmsUtil.SendSMS(phoneNumberArray, content, _configuration);
        
        var smsResponse = JsonSerializer.Deserialize<SmsModel.SmsResponse>(response);
        if (smsResponse.status != "success" && smsResponse.code != "00")
        {
            throw new BadHttpRequestException("Lỗi khi gửi mã OTP");
        }
        
        await _redisService.SetStringAsync(key, otp, TimeSpan.FromMinutes(2));
        return request.PhoneNumber;
    }
}