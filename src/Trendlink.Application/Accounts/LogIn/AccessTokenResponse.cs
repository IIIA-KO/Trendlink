namespace Trendlink.Application.Accounts.LogInUser
{
    public sealed record AccessTokenResponse(
        string AccessToken,
        string RefreshToken,
        int ExpiresIn
    );
}
