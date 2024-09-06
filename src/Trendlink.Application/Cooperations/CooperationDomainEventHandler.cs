using System.Text;
using MediatR;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Cooperations.DomainEvents;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Cooperations
{
    internal abstract class CooperationDomainEventHandler<TDomainEvent>
        : INotificationHandler<TDomainEvent>
        where TDomainEvent : ICooperationDomainEvent
    {
        protected readonly ICooperationRepository _cooperationRepository;
        protected readonly IUserRepository _userRepository;
        protected readonly IAdvertisementRepository _advertisementRepository;
        protected readonly IDateTimeProvider _dateTimeProvider;
        protected readonly INotificationRepository _notificationRepository;
        protected readonly IUnitOfWork _unitOfWork;

        protected abstract CompositeFormat MessageFormat { get; }

        protected CooperationDomainEventHandler(
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

        public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
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

            User? user = await this.GetUserAsync(cooperation, cancellationToken);
            if (user is null)
            {
                return;
            }

            string message = this.GenerateMessage(advertisement, user);

            Notification builtNotification = NotificationBuilder
                .ForUser(this.GetReceiverId(cooperation))
                .WithType(NotificationType.System)
                .WithTitle(this.GetNotificationTitle())
                .WithMessage(message)
                .CreatedOn(this._dateTimeProvider.UtcNow)
                .Build();

            this._notificationRepository.Add(builtNotification);
            await this._unitOfWork.SaveChangesAsync(cancellationToken);
        }

        protected abstract Task<User?> GetUserAsync(
            Cooperation cooperation,
            CancellationToken cancellationToken
        );

        protected abstract UserId GetReceiverId(Cooperation cooperation);

        protected abstract string GenerateMessage(Advertisement advertisement, User user);

        protected abstract string GetNotificationTitle();
    }
}
