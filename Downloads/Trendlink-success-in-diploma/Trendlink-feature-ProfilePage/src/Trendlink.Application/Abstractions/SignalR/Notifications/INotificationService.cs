namespace Trendlink.Application.Abstractions.SignalR.Notifications
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string userId, string title, string message);
    }
}
