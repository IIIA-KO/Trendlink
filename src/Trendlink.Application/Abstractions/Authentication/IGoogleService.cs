namespace Trendlink.Application.Abstractions.Authentication
{
    public interface IGoogleService
    {
        Task<string?> GetAccessTokenAsync(string code, CancellationToken cancellationToken);

        Task<UserInfo?> GetUserInfoAsync(string accessToken, CancellationToken cancellationToken);
    }
}
