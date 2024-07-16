using System.Globalization;
using System.Text;
using MediatR;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Notifications.ValueObjects;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.DomainEvents;

namespace Trendlink.Application.Users.RegisterUser
{
    internal sealed class UserCreatedDomainEventHandler
        : INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        private static readonly CompositeFormat WelcomeMessageFormat = CompositeFormat.Parse(
            Resources.NotificationMessages.WelcomeMessage
        );

        public UserCreatedDomainEventHandler(
            IUserRepository userRepository,
            IDateTimeProvider dateTimeProvider,
            INotificationRepository notificationRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._userRepository = userRepository;
            this._dateTimeProvider = dateTimeProvider;
            this._notificationRepository = notificationRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task Handle(
            UserCreatedDomainEvent notification,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByIdAsync(
                notification.UserId,
                cancellationToken
            );
            if (user is null)
            {
                return;
            }

            string welcomeMessage = string.Format(
                CultureInfo.CurrentCulture,
                WelcomeMessageFormat,
                user.FirstName.Value
            );

            Notification greetingMessage = Notification
                .Create(
                    user.Id,
                    NotificationType.News,
                    new Title("Welcome to Trendlink!"),
                    new Message(welcomeMessage),
                    this._dateTimeProvider.UtcNow
                )
                .Value;

            this._notificationRepository.Add(greetingMessage);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
