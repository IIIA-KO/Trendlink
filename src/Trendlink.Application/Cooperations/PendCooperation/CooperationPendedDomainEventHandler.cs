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
using Trendlink.Domain.Users;

namespace Trendlink.Application.Cooperations.PendCooperation
{
    internal sealed class CooperationPendedDomainEventHandler
        : INotificationHandler<CooperationPendedDomainEvent>
    {
        private static readonly CompositeFormat MessageFormat = CompositeFormat.Parse(
            Resources.NotificationMessages.CooperationPending
        );

        private readonly ICooperationRepository _cooperationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CooperationPendedDomainEventHandler(
            ICooperationRepository cooperationRepository,
            IUserRepository userRepository,
            IAdvertisementRepository advertisementRepository,
            IDateTimeProvider dateTimeProvider,
            INotificationRepository notificationRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._cooperationRepository = cooperationRepository;
            this._userRepository = userRepository;
            this._advertisementRepository = advertisementRepository;
            this._dateTimeProvider = dateTimeProvider;
            this._notificationRepository = notificationRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task Handle(
            CooperationPendedDomainEvent notification,
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

            User? seller = await this._userRepository.GetByIdAsync(
                cooperation.SellerId,
                cancellationToken
            );
            if (seller is null)
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

            string cooperationPendedMessage = string.Format(
                CultureInfo.CurrentCulture,
                MessageFormat,
                seller.FirstName.Value,
                advertisement.Name.Value
            );

            Result<Notification> result = Notification.Create(
                cooperation.SellerId,
                NotificationType.System,
                new Title("New Cooperation Request"),
                new Message(cooperationPendedMessage),
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
