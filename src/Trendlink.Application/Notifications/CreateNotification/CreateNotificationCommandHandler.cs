using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Notifications.ValueObjects;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Notifications.CreateNotification
{
    internal sealed class CreateNotificationCommandHandler
        : ICommandHandler<CreateNotificationCommand, NotificationId>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserRepository _userRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateNotificationCommandHandler(
            IDateTimeProvider dateTimeProvider,
            IUserRepository userRepository,
            INotificationRepository notificationRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._dateTimeProvider = dateTimeProvider;
            this._userRepository = userRepository;
            this._notificationRepository = notificationRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<NotificationId>> Handle(
            CreateNotificationCommand request,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
            {
                return Result.Failure<NotificationId>(UserErrors.NotFound);
            }

            Result<Notification> result = Notification.Create(
                user.Id,
                request.NotificationType,
                request.Title,
                request.Message,
                this._dateTimeProvider.UtcNow
            );
            if (result.IsFailure)
            {
                return Result.Failure<NotificationId>(result.Error);
            }

            Notification notification = result.Value;

            this._notificationRepository.Add(notification);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return notification.Id;
        }
    }
}
