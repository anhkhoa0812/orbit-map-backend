using System.Text.Json;
using AutoMapper;
using OrbitMap.API.Payload.Response.Device;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class NotificationService : BaseService<NotificationService>
{
    private readonly IOneSignalService _oneSignalService;
    private readonly IConfiguration _config;
    private readonly IRedisService _redisService;

    public NotificationService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IOneSignalService oneSignalService, IConfiguration config,
        IRedisService redisService) : base(
        unitOfWork, logger, mapper, httpContextAccessor)
    {
        _oneSignalService = oneSignalService;
        _config = config;
        _redisService = redisService;
    }

    public async Task SendNotificationToUser(string senderUsername, string recipientUsername, string message)
    {
        var key = $"DeviceTracker:{senderUsername}";
        var deviceJson = await _redisService.GetStringAsync(key);
        var device = JsonSerializer.Deserialize<DeviceInfo>(deviceJson);
        if (device != null)
        {
            var messageBody = message;
            var obj = new
            {
                android_channel_id = _config["OneSignal:AndroidChannelId"],
                app_id = _config["OneSignal:AppId"],
                headings = new { en = "Social app", es = "Title Spanish Message" },
                contents = new { en = messageBody, es = "Spanish Message body" },
                include_subscription_ids = device.SubscriptionId,
                name = "INTERNAL_CAMPAIGN_NAME"
            };
            await _oneSignalService.SendNotification(obj);
        }
    }
}