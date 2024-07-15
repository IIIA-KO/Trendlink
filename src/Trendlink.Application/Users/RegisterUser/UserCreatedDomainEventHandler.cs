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

            Notification greetingMessage = Notification
                .Create(
                    user.Id,
                    NotificationType.News,
                    new Title("Welcome to Trendlink!"),
                    new Message(
                        """
                        Hello, {user.FirstName.Value}
                         
                        Welcome to Trendlink, the ultimate platform for bloggers connect, collaborate and grow!

                        We're thrilled to have you join our community. At Trendlink, we believe in the power of collaboration and magic that happens when like-minded individuals come together.

                        Here are few things you can do to get started:
                        1. Complete your profile: Let the community know more about you.
                        2. Explore collaboration opportunities: Browse through current cooperation requests or make your own.
                        3. Connect with others: Follow your favourite bloggers and start building your network.

                        Thank you for joining us!

                        Best regards,
                        The Trendlink team.
                        """
                    ),
                    this._dateTimeProvider.UtcNow
                )
                .Value;

            this._notificationRepository.Add(greetingMessage);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
