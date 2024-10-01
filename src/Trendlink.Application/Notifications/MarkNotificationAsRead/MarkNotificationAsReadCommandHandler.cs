using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Notifications.MarkNotificationAsRead
{
    internal class MarkNotificationAsReadCommandHandler
        : ICommandHandler<MarkNotificationAsReadCommand>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public MarkNotificationAsReadCommandHandler(
            INotificationRepository notificationRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork
        )
        {
            this._notificationRepository = notificationRepository;
            this._userContext = userContext;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            MarkNotificationAsReadCommand request,
            CancellationToken cancellationToken
        )
        {
            Notification? notification = await this._notificationRepository.GetByIdAsync(
                request.NotificationId,
                cancellationToken
            );
            if (notification is null)
            {
                return Result.Failure(NotificationErrors.NotFound);
            }

            if (this._userContext.UserId != notification.UserId)
            {
                return Result.Failure(UserErrors.NotAuthorized);
            }

            notification.MarkAsRead();

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
