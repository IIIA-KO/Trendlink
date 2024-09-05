namespace Trendlink.Infrastructure.Authentication.Models
{
    public record CachedAuthorizationToken(
        string AccessToken,
        string RefreshToken,
        DateTimeOffset AcquiredAt,
        int ExpiresIn
    );
}
