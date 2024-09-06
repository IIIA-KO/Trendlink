using System.Globalization;
using System.Text;
using MediatR;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Notifications;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.DomainEvents;

namespace Trendlink.Application.Users.RegisterUser
{
    internal sealed class UserCreatedDomainEventHandler
        : INotificationHandler<UserCreatedDomainEvent>
    {
        private static readonly CompositeFormat MessageFormat = CompositeFormat.Parse(
            Resources.NotificationMessages.WelcomeMessage
        );

        private readonly IUserRepository _userRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

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
                MessageFormat,
                user.FirstName.Value
            );

            Notification builtNotification = NotificationBuilder
                .ForUser(user.Id)
                .WithType(NotificationType.System)
                .WithTitle("Welcome to Trendlink!")
                .WithMessage(welcomeMessage)
                .CreatedOn(this._dateTimeProvider.UtcNow)
                .Build();

            this._notificationRepository.Add(builtNotification);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
