using Microsoft.EntityFrameworkCore;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Users.VerificationTokens;
using Trendlink.Infrastructure.Specifications.EmailVerificationTokens;

namespace Trendlink.Infrastructure.Repositories
{
    internal sealed class EmailVerificationTokenRepository
        : Repository<EmailVerificationToken, EmailVerificationTokenId>,
            IEmailVerificationTokenRepository
    {
        public EmailVerificationTokenRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<EmailVerificationToken?> GetByIdWithUser(
            EmailVerificationTokenId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this.ApplySpecification(
                    new EmailVerificationTokenByIdWithUserSpecification(id)
                )
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
