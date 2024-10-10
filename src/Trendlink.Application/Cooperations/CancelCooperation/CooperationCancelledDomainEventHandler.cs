using System.Globalization;
using System.Text;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Emails;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Cooperations.DomainEvents;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Cooperations.CancelCooperation
{
    internal sealed class CooperationCancelledDomainEventHandler
        : CooperationDomainEventHandler<CooperationCancelledDomainEvent>
    {
        public CooperationCancelledDomainEventHandler(
            ICooperationRepository cooperationRepository,
            IUserRepository userRepository,
            IAdvertisementRepository advertisementRepository,
            IDateTimeProvider dateTimeProvider,
            IEmailService emailService
        )
            : base(
                cooperationRepository,
                userRepository,
                advertisementRepository,
                dateTimeProvider,
                emailService
            ) { }

        protected override CompositeFormat MessageFormat =>
            CompositeFormat.Parse(Resources.NotificationMessages.CooperationCancelled);

        protected override string GenerateMessage(Advertisement advertisement, User user)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                this.MessageFormat,
                advertisement.Name.Value,
                user.FirstName.Value
            );
        }

        protected override string GetEmailSubject() => "Cooperation Cancelled";

        protected override async Task<Email> GetRecipientEmail(Cooperation cooperation)
        {
            User? user = await this._userRepository.GetByIdAsync(cooperation.BuyerId);

            return user!.Email;
        }

        protected override async Task<User?> GetUserAsync(
            Cooperation cooperation,
            CancellationToken cancellationToken
        ) => await this._userRepository.GetByIdAsync(cooperation.BuyerId, cancellationToken);
    }
}
