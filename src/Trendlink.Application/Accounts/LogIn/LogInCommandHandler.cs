﻿using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.VerificationTokens;

namespace Trendlink.Application.Accounts.LogIn
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
            User? user = await this._userRepository.GetByEmailAsync(
                request.Email,
                cancellationToken
            );
            if (user is null)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.NotFound);
            }
            if (!user.EmailVerified)
            {
                return Result.Failure<AccessTokenResponse>(
                    EmailVerificationTokenErrors.EmailNotVerified
                );
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
