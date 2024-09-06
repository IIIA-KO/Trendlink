namespace Trendlink.Infrastructure.Authentication.Keycloak
{
    internal record CachedAuthorizationToken(
        string AccessToken,
        string RefreshToken,
        DateTimeOffset AcquiredAt,
        int ExpiresIn
    );
}
