using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.LogInUser
{
    internal sealed class LogInUserCommandHandler
        : ICommandHandler<LogInUserCommand, AccessTokenResponse>
    {
        private readonly IKeycloakService _jwtService;

        public LogInUserCommandHandler(IKeycloakService jwtService)
        {
            this._jwtService = jwtService;
        }

        public async Task<Result<AccessTokenResponse>> Handle(
            LogInUserCommand request,
            CancellationToken cancellationToken
        )
        {
            Result<AccessTokenResponse> result = await this._jwtService.GetAccessTokenAsync(
                request.Email,
                request.Password,
                cancellationToken
            );

            if (result.IsFailure)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
            }

            return result;
        }
    }
}
