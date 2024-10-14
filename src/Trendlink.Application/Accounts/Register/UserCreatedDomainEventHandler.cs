using MediatR;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Emails;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.DomainEvents;
using Trendlink.Domain.Users.VerificationTokens;

namespace Trendlink.Application.Accounts.Register
{
    internal sealed class UserCreatedDomainEventHandler
        : INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IEmailVerificationLinkFactory _emailVerificationLinkFactory;
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserCreatedDomainEventHandler(
            IUserRepository userRepository,
            IEmailService emailService,
            IDateTimeProvider dateTimeProvider,
            IEmailVerificationLinkFactory emailVerificationLinkFactory,
            IEmailVerificationTokenRepository emailVerificationTokenRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._userRepository = userRepository;
            this._emailService = emailService;
            this._emailVerificationLinkFactory = emailVerificationLinkFactory;
            this._emailVerificationTokenRepository = emailVerificationTokenRepository;
            this._dateTimeProvider = dateTimeProvider;
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

            DateTime utcNow = this._dateTimeProvider.UtcNow;
            var emailVerificationToken = new EmailVerificationToken(
                user.Id,
                utcNow,
                utcNow.AddDays(1)
            );
            this._emailVerificationTokenRepository.Add(emailVerificationToken);

            user.VerifyEmail(emailVerificationToken);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            string verificationLink = this._emailVerificationLinkFactory.Create(
                emailVerificationToken
            );

            await this._emailService.SendAsync(
                user.Email,
                "Email Verification for Trendlink",
                $"To verify your email <a href='{verificationLink}'>click here</a>",
                isHtml: true
            );
        }
    }
}
