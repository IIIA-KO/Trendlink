using Trendlink.Domain.Users.VerificationTokens;

namespace Trendlink.Infrastructure.Specifications.EmailVerificationTokens
{
    internal sealed class EmailVerificationTokenByIdWithUserSpecification
        : Specification<EmailVerificationToken, EmailVerificationTokenId>
    {
        public EmailVerificationTokenByIdWithUserSpecification(EmailVerificationTokenId tokenId)
            : base(token => token.Id == tokenId)
        {
            this.AddInclude(token => token.User);
        }
    }
}
