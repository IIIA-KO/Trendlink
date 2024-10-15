using System.Globalization;
using System.Text;
using MediatR;
using Trendlink.Application.Abstractions.Emails;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.VerificationTokens;
using Trendlink.Domain.Users.VerificationTokens.DomainEvents;

namespace Trendlink.Application.Accounts.VerifyEmail
{
    internal sealed class EmailVerifiedDomainEventHandler
        : INotificationHandler<EmailVerifiedDomainEvent>
    {
        private static readonly CompositeFormat MessageFormat = CompositeFormat.Parse(
            Resources.NotificationMessages.WelcomeMessage
        );

        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public EmailVerifiedDomainEventHandler(
            IEmailVerificationTokenRepository emailVerificationTokenRepository,
            IUserRepository userRepository,
            IEmailService emailService,
            IUnitOfWork unitOfWork
        )
        {
            this._emailVerificationTokenRepository = emailVerificationTokenRepository;
            this._userRepository = userRepository;
            this._emailService = emailService;
            this._unitOfWork = unitOfWork;
        }

        public async Task Handle(
            EmailVerifiedDomainEvent notification,
            CancellationToken cancellationToken
        )
        {
            EmailVerificationToken? token =
                await this._emailVerificationTokenRepository.GetByIdAsync(
                    notification.EmailVerificationTokenId,
                    cancellationToken
                );
            if (token is null)
            {
                return;
            }

            User? user = await this._userRepository.GetByIdAsync(token.UserId, cancellationToken);
            if (user is null)
            {
                return;
            }

            this._emailVerificationTokenRepository.Remove(token);
            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            string welcomeMessage = string.Format(
                CultureInfo.CurrentCulture,
                MessageFormat,
                user.FirstName.Value
            );

            await this._emailService.SendAsync(user.Email, "Welcome to Trendlink", welcomeMessage);
        }
    }
}
