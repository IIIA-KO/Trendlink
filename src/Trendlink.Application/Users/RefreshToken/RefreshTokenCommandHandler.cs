using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Users.LogInUser;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Users.RefreshToken
{
    internal sealed class RefreshTokenCommandHandler
        : ICommandHandler<RefreshTokenCommand, AccessTokenResponse>
    {
        private readonly IJwtService _jwtService;

        public RefreshTokenCommandHandler(IJwtService jwtService)
        {
            this._jwtService = jwtService;
        }

        public async Task<Result<AccessTokenResponse>> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken
        )
        {
            Result<AccessTokenResponse> result = await this._jwtService.RefreshTokenAsync(
                request.RefreshToken,
                cancellationToken
            );

            if (result.IsFailure)
            {
                return Result.Failure<AccessTokenResponse>(result.Error);
            }

            return result;
        }
    }
}
