namespace Trendlink.Infrastructure.BackgroundJobs.Token
{
    internal sealed record UserTokenResponse(
        Guid Id,
        Guid UserId,
        string AccessToken,
        DateTimeOffset ExpiresAtUtc
    )
    {
        public UserTokenResponse()
            : this(Guid.Empty, Guid.Empty, string.Empty, DateTimeOffset.UtcNow) { }
    }
}
