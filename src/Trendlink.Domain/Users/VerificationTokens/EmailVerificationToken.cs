using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users.VerificationTokens
{
    public sealed class EmailVerificationToken : Entity<EmailVerificationTokenId>
    {
        private EmailVerificationToken() { }

        public UserId UserId { get; private set; }

        public User User { get; init; }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime ExpiresAtUtc { get; private set; }

        public EmailVerificationToken(UserId userId, DateTime createdOnUtc, DateTime expiresAtUtc)
            : base(EmailVerificationTokenId.New())
        {
            this.UserId = userId;
            this.CreatedOnUtc = createdOnUtc;
            this.ExpiresAtUtc = expiresAtUtc;
        }
    }
}
