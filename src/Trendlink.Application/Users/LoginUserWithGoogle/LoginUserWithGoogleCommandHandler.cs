﻿using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Users.LogInUser;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Users.GoogleLogin
{
    internal sealed class LogInUserWithGoogleCommandHandler
        : ICommandHandler<LogInUserWithGoogleCommand, AccessTokenResponse>
    {
        private readonly IGoogleService _googleService;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public LogInUserWithGoogleCommandHandler(
            IGoogleService googleService,
            IUserRepository userRepository,
            IJwtService jwtService
        )
        {
            this._jwtService = jwtService;
            this._userRepository = userRepository;
            this._googleService = googleService;
        }

        public async Task<Result<AccessTokenResponse>> Handle(
            LogInUserWithGoogleCommand request,
            CancellationToken cancellationToken
        )
        {
            string? accessToken = await this._googleService.GetAccessTokenAsync(
                request.Code,
                cancellationToken
            );
            if (accessToken is null)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
            }

            UserInfo? userInfo = await this._googleService.GetUserInfoAsync(
                accessToken,
                cancellationToken
            );
            if (userInfo is null)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
            }

            try
            {
                bool userExists = await this._userRepository.ExistByEmailAsync(
                    new Email(userInfo.Email),
                    cancellationToken
                );
                if (!userExists)
                {
                    return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
                }

                Result<AccessTokenResponse> tokenResult =
                    await this._jwtService.AuthenticateWithGoogleAsync(userInfo, cancellationToken);
                if (tokenResult.IsFailure)
                {
                    return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
                }

                return tokenResult.Value;
            }
            catch (Exception exception)
                when (exception is HttpRequestException || exception is ArgumentNullException)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.RegistrationFailed);
            }
        }
    }
}
