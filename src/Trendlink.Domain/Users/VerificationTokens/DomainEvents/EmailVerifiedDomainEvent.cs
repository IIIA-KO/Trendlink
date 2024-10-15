using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users.VerificationTokens.DomainEvents
{
    public sealed record EmailVerifiedDomainEvent(EmailVerificationTokenId EmailVerificationTokenId)
        : IDomainEvent;
}
