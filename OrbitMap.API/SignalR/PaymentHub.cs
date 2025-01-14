using Microsoft.AspNetCore.SignalR;

namespace OrbitMap.API.SignalR;

public class PaymentHub : Hub
{
    public async Task JoinPaymentGroup(string orderCode)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, orderCode);
    }

    public async Task LeavePaymentGroup(string orderCode)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, orderCode);
    }
}