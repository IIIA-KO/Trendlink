using System.Globalization;
using System.Text;
using MediatR;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations.DomainEvents;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Notifications.ValueObjects;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Cooperations.CancelCooperation
{
    internal sealed class CooperationCancelledDomainEventHandler
        : INotificationHandler<CooperationsCancelledDomainEvent>
    {
        private static readonly CompositeFormat MessageFormat = CompositeFormat.Parse(
            Resources.NotificationMessages.CooperationCancelled
        );

        private readonly IUserRepository _userRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CooperationCancelledDomainEventHandler(
            IUserRepository userRepository,
            IAdvertisementRepository advertisementRepository,
            IDateTimeProvider dateTimeProvider,
            INotificationRepository notificationRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._userRepository = userRepository;
            this._advertisementRepository = advertisementRepository;
            this._dateTimeProvider = dateTimeProvider;
            this._notificationRepository = notificationRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task Handle(
            CooperationsCancelledDomainEvent notification,
            CancellationToken cancellationToken
        )
        {
            Advertisement? advertisement = await this._advertisementRepository.GetByIdAsync(
                notification.Cooperation.AdvertisementId,
                cancellationToken
            );
            if (advertisement is null)
            {
                return;
            }

            User? buyer = await this._userRepository.GetByIdAsync(
                notification.Cooperation.BuyerId,
                cancellationToken
            );
            if (buyer is null)
            {
                return;
            }

            string cooperationCancelledMessage = string.Format(
                CultureInfo.CurrentCulture,
                MessageFormat,
                advertisement.Name.Value,
                buyer.FirstName.Value
            );

            Result<Notification> result = Notification.Create(
                notification.Cooperation.SellerId,
                NotificationType.Message,
                new Title("Cooperation Cancelled"),
                new Message(cooperationCancelledMessage),
                this._dateTimeProvider.UtcNow
            );

            if (result.IsFailure)
            {
                return;
            }

            this._notificationRepository.Add(result.Value);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
