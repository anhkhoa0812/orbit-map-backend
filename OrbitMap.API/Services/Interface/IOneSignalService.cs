using OrbitMap.API.Payload.Response.OneSignal;

namespace OrbitMap.API.Services.Interface;

public interface IOneSignalService
{
   Task<ResultOneSignal> SendNotification(object body);
   Task<PlayerIdsDto> AddPlayerId(string playerId, string username);
}