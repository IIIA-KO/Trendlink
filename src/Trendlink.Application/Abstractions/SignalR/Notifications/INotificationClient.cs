namespace Trendlink.Application.Abstractions.SignalR.Notifications
{
    public interface INotificationClient
    {
        Task ReceiveNotification(string title, string message);
    }
}
