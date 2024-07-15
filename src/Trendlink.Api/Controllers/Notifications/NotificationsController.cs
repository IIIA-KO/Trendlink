using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Notifications.CreateNotification;
using Trendlink.Application.Notifications.MarkNotificationAsRead;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Notifications.ValueObjects;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Api.Controllers.Notifications
{
    [Route("/api/notifications")]
    public class NotificationsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateNotification(
            [FromBody] СreateNotificationRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new CreateNotificationCommand(
                new UserId(request.UserId),
                (NotificationType)request.NotificationType,
                new Title(request.Title),
                new Message(request.Message)
            );

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> MarkNotificationAsRead(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var command = new MarkNotificationAsReadCommand(new NotificationId(id));

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }
    }
}
