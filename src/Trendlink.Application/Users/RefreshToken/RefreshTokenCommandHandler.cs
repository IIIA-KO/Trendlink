using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Users.LogInUser;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Users.RefreshToken
{
    internal sealed class RefreshTokenCommandHandler
        : ICommandHandler<RefreshTokenCommand, AccessTokenResponse>
    {
        private readonly IKeycloakService _keycloakService;

        public RefreshTokenCommandHandler(IKeycloakService keycloakService)
        {
            this._keycloakService = keycloakService;
        }

        public async Task<Result<AccessTokenResponse>> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken
        )
        {
            Result<AccessTokenResponse> result = await this._keycloakService.RefreshTokenAsync(
                request.RefreshToken,
                cancellationToken
            );

            if (result is null || result.IsFailure)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
            }

            return result;
        }
    }
}
