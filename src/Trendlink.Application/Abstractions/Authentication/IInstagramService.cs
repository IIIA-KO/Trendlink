namespace Trendlink.Application.Abstractions.Authentication
{
    public interface IInstagramService
    {
        Task<(string?, long?)> GetAccessTokenAsync(
            string code,
            CancellationToken cancellationToken = default
        );

        Task<InstagramUserInfo?> GetUserInfoAsync(
            string accessToken,
            long instagramUserId,
            CancellationToken cancellationToken = default
        );
    }
}
