namespace Trendlink.Application.Accounts.LogIn
{
    public sealed record AccessTokenResponse(
        string AccessToken,
        string RefreshToken,
        int ExpiresIn
    );
}
