using Trendlink.Domain.Abstraction;
using AccessTokenResponse = Trendlink.Application.Users.LogInUser.AccessTokenResponse;

namespace Trendlink.Application.Abstractions.Authentication
{
    public interface IJwtService
    {
        Task<Result<AccessTokenResponse>> GetAccessTokenAsync(
            string email,
            string password,
            CancellationToken cancellationToken = default
        );

        Task<Result<AccessTokenResponse>> RefreshTokenAsync(
            string refreshToken,
            CancellationToken cancellationToken = default
        );
    }
}
