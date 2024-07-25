using System.Globalization;
using System.Text;
using MediatR;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Cooperations.DomainEvents;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Notifications.ValueObjects;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Cooperations.MarkCooperationAsDone
{
    internal class CooperationDoneDomainEventHandler
        : INotificationHandler<CooperationDoneDomainEvent>
    {
        private static readonly CompositeFormat MessageFormat = CompositeFormat.Parse(
            Resources.NotificationMessages.CooperationDone
        );

        private readonly ICooperationRepository _cooperationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CooperationDoneDomainEventHandler(
            ICooperationRepository cooperationRepository,
            IUserRepository userRepository,
            IDateTimeProvider dateTimeProvider,
            INotificationRepository notificationRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._cooperationRepository = cooperationRepository;
            this._userRepository = userRepository;
            this._dateTimeProvider = dateTimeProvider;
            this._notificationRepository = notificationRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task Handle(
            CooperationDoneDomainEvent notification,
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

            string cooperationDoneMessage = string.Format(
                CultureInfo.CurrentCulture,
                MessageFormat,
                seller.FirstName.Value
            );

            Result<Notification> result = Notification.Create(
                cooperation.BuyerId,
                NotificationType.Message,
                new Title("Advertisement Done!"),
                new Message(cooperationDoneMessage),
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
