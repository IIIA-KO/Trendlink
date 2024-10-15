using Trendlink.Domain.Users;
using Trendlink.Domain.Users.VerificationTokens;

namespace Trendlink.Application.Abstractions.Repositories
{
    public interface IEmailVerificationTokenRepository
    {
        Task<EmailVerificationToken?> GetByIdAsync(
            EmailVerificationTokenId id,
            CancellationToken cancellationToken = default
        );

        Task<EmailVerificationToken?> GetByIdWithUser(
            EmailVerificationTokenId id,
            CancellationToken cancellationToken = default
        );

        Task<EmailVerificationToken?> GetActiveTokenByUserId(
            UserId userId,
            CancellationToken cancellationToken
        );

        void Add(EmailVerificationToken emailVerificationToken);

        void Remove(EmailVerificationToken emailVerificationToken);
    }
}
