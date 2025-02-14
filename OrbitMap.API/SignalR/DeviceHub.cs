using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OrbitMap.API.Helper;
using OrbitMap.API.Services.Interface;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.SignalR;

[Authorize]
public class DeviceHub : Hub
{
    private readonly DeviceTracker _tracker;
    private readonly ILogger _logger;

    public DeviceHub(DeviceTracker tracker, ILogger logger)
    {
        _tracker = tracker;
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        var username = Context.User.GetUsername();
        if (string.IsNullOrEmpty(username))
        {
            Context.Abort();
            return;
        }

        var httpContext = Context.GetHttpContext();
        var subscriptionId = httpContext?.Request.Query["subscriptionId"];
        if (string.IsNullOrEmpty(subscriptionId))
        {
            Context.Abort();
            return;
        }

        // Context.Items["subscriptionId"] = subscriptionId;

        var previousDevice = await _tracker.RegisterDeviceAsync(username, subscriptionId, Context.ConnectionId);
        if (previousDevice != null && !previousDevice.SubscriptionId.Equals(subscriptionId))
        {
            _logger.Information(
                $"User {username} has logged in from another device. SubscriptionId: {previousDevice.SubscriptionId}");
            await Clients.Client(previousDevice.ConnectionId)
                .SendAsync("ForceLogout", "A new device has connected with your account.");
        }

        await base.OnConnectedAsync();
    }

    // public override async Task OnDisconnectedAsync(Exception? exception)
    // {
    //     var username = Context.User.GetUsername();
    //
    //     if (!string.IsNullOrEmpty(username) && Context.Items.TryGetValue("subscriptionId", out var subIdObj) &&
    //         subIdObj is string subscriptionId)
    //     {
    //         await _tracker.UnregisterDeviceAsync(username, subscriptionId, Context.ConnectionId);
    //     }
    //
    //     await base.OnDisconnectedAsync(exception);
    // }
}