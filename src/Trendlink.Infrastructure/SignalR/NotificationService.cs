using Microsoft.AspNetCore.SignalR;
using Trendlink.Application.Abstractions.SignalR.Notifications;

namespace Trendlink.Infrastructure.SignalR
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;

        public NotificationService(IHubContext<NotificationHub, INotificationClient> hubContext)
        {
            this._hubContext = hubContext;
        }

        public async Task SendNotificationAsync(string userId, string title, string message)
        {
            await this._hubContext.Clients.User(userId).ReceiveNotification(title, message);
        }
    }
}
