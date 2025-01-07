using AutoMapper;
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
    public NotificationService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IOneSignalService oneSignalService, IConfiguration config) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
        _oneSignalService = oneSignalService;
        _config = config;
    }
    
    public async Task SendNotificationToUser(string senderUsername, string recipientUsername, string message)
    {
        var toPlayerIds = await _unitOfWork.GetRepository<PlayerIds>().GetListAsync(
            predicate: x => x.Username == recipientUsername
        );
        var toIds = toPlayerIds.Select(x => x.PlayerId).ToArray();
        if (toIds.Length > 0)
        {
            var messageBody = message;
            var obj = new
            {
                android_channel_id = _config["OneSignal:AndroidChannelId"],
                app_id = _config["OneSignal:AppId"],
                headings = new { en = "Social app", es = "Title Spanish Message" },
                contents = new { en = messageBody, es = "Spanish Message body" },
                include_player_ids = toIds,
                name = "INTERNAL_CAMPAIGN_NAME"
            };
            await _oneSignalService.SendNotification(obj);
        }
    }
    
}