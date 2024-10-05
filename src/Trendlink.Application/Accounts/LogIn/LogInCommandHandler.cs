using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Accounts.LogInUser
{
    internal sealed class LogInCommandHandler : ICommandHandler<LogInCommand, AccessTokenResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IKeycloakService _keycloakService;

        public LogInCommandHandler(IUserRepository userRepository, IKeycloakService keycloakService)
        {
            this._userRepository = userRepository;
            this._keycloakService = keycloakService;
        }

        public async Task<Result<AccessTokenResponse>> Handle(
            LogInCommand request,
            CancellationToken cancellationToken
        )
        {
            bool userExists = await this._userRepository.ExistByEmailAsync(
                request.Email,
                cancellationToken
            );
            if (!userExists)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.NotFound);
            }

            Result<AccessTokenResponse> result = await this._keycloakService.GetAccessTokenAsync(
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
