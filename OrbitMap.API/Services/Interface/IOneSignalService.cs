using OrbitMap.API.Payload.Response.OneSignal;

namespace OrbitMap.API.Services.Interface;

public interface IOneSignalService
{
    Task<ResultOneSignal> SendNotification(object body);
    Task<SubscriptionIdsDto> AddSubscriptionIdAsync(string subscriptionId, string username);

    Task<SubscriptionIdsDto> RemoveSubscriptionIdAsync(string subscriptionId, string username);
}