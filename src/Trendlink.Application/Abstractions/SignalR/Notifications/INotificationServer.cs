namespace Trendlink.Application.Abstractions.SignalR.Notifications
{
    public interface INotificationServer
    {
        Task SendNotificationAsync(string userId, string title, string message);
    }
}
