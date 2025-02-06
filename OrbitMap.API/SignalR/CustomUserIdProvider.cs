using Microsoft.AspNetCore.SignalR;
using OrbitMap.API.Helper;

namespace OrbitMap.API.SignalR;

public class CustomUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User?.GetUsername();
    }
}