namespace Trendlink.Application.Users.Authentication.LogInUser
{
    public sealed record AccessTokenResponse(
        string AccessToken,
        string RefreshToken,
        int ExpiresIn
    );
}
