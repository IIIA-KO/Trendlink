using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Domain.Users.DomainEvents
{
    public sealed record InstagramAccountLinkedDomainEvent(
        UserId UserId,
        InstagramAccount InstagramAccount,
        string FacebookAccessToken,
        DateTimeOffset ExpiresAt
    ) : IDomainEvent;
}
