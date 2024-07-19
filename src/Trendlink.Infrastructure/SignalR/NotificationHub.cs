using Microsoft.AspNetCore.SignalR;
using Trendlink.Application.Abstractions.SignalR.Notifications;

namespace Trendlink.Infrastructure.SignalR
{
    public class NotificationHub : Hub<INotificationClient>, INotificationServer
    {
        public async Task SendNotificationAsync(string userId, string title, string message)
        {
            await this.Clients.User(userId).ReceiveNotification(title, message);
        }
    }
}
