using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Abstractions.SignalR.Notifications;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Notifications.CreateNotification
{
    internal sealed class CreateNotificationCommandHandler
        : ICommandHandler<CreateNotificationCommand, NotificationId>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserRepository _userRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateNotificationCommandHandler(
            IDateTimeProvider dateTimeProvider,
            IUserRepository userRepository,
            INotificationRepository notificationRepository,
            INotificationService notificationService,
            IUnitOfWork unitOfWork
        )
        {
            this._dateTimeProvider = dateTimeProvider;
            this._userRepository = userRepository;
            this._notificationRepository = notificationRepository;
            this._notificationService = notificationService;
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

            await this._notificationService.SendNotificationAsync(
                user.Id.Value.ToString(),
                notification.Title.Value,
                notification.Message.Value
            );

            return notification.Id;
        }
    }
}
