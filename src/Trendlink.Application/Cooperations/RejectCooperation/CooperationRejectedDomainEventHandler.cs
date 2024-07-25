using System.Globalization;
using System.Text;
using MediatR;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;
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

        private readonly ICooperationRepository _cooperationRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CooperationRejectedDomainEventHandler(
            ICooperationRepository cooperationRepository,
            IAdvertisementRepository advertisementRepository,
            IDateTimeProvider dateTimeProvider,
            INotificationRepository notificationRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._cooperationRepository = cooperationRepository;
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
            Cooperation? cooperation = await this._cooperationRepository.GetByIdAsync(
                notification.CooperationId,
                cancellationToken
            );
            if (cooperation is null)
            {
                return;
            }

            Advertisement? advertisement = await this._advertisementRepository.GetByIdAsync(
                cooperation.AdvertisementId,
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
                cooperation.BuyerId,
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
