using System.Globalization;
using System.Text;
using MediatR;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations.DomainEvents;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Notifications.ValueObjects;

namespace Trendlink.Application.Cooperations.RejectCooperation
{
    internal sealed class CooperationRejectedDomainEventHandler
        : INotificationHandler<CooperationRejectedDomainEvent>
    {
        private static readonly CompositeFormat MessageFormat = CompositeFormat.Parse(
            Resources.NotificationMessages.CooperationRejected
        );

        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CooperationRejectedDomainEventHandler(
            IAdvertisementRepository advertisementRepository,
            IDateTimeProvider dateTimeProvider,
            INotificationRepository notificationRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._advertisementRepository = advertisementRepository;
            this._dateTimeProvider = dateTimeProvider;
            this._notificationRepository = notificationRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task Handle(
            CooperationRejectedDomainEvent notification,
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

            string cooperationRejectedMessage = string.Format(
                CultureInfo.CurrentCulture,
                MessageFormat,
                advertisement.Name.Value
            );

            Result<Notification> result = Notification.Create(
                notification.Cooperation.BuyerId,
                NotificationType.Message,
                new Title("Cooperation Rejected"),
                new Message(cooperationRejectedMessage),
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
