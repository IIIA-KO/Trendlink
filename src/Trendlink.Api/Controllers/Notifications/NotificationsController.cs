using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Notifications.CreateNotification;
using Trendlink.Application.Notifications.GetLoggedInUserNotifications;
using Trendlink.Application.Notifications.MarkNotificationAsRead;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users;

namespace Trendlink.Api.Controllers.Notifications
{
    [Route("/api/notifications")]
    public class NotificationsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetLoggedInUserNotifications(
            [FromQuery] string? sortColumn,
            [FromQuery] string? sortOrder,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default
        )
        {
            var query = new GetLoggedInUserNotificationsQuery(
                sortColumn,
                sortOrder,
                pageNumber,
                pageSize
            );

            return this.HandlePagedResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpPost]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> CreateNotification(
            [FromBody] СreateNotificationRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new CreateNotificationCommand(
                new UserId(request.UserId),
                request.NotificationType,
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
