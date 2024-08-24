using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Users.LogInUser;
using Trendlink.Application.Users.LoginUserWithGoogle;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Users
{
    public class LogInUserWithGoogleTests
    {
        private static readonly LogInUserWithGoogleCommand Command = new(UserData.Code);

        private readonly IGoogleService _googleServiceMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IKeycloakService _jwtServiceMock;

        private readonly LogInUserWithGoogleCommandHandler _handler;

        public LogInUserWithGoogleTests()
        {
            this._googleServiceMock = Substitute.For<IGoogleService>();
            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._jwtServiceMock = Substitute.For<IKeycloakService>();

            this._handler = new LogInUserWithGoogleCommandHandler(
                this._googleServiceMock,
                this._userRepositoryMock,
                this._jwtServiceMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenAccessTokenIsNull()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns((GoogleTokenResponse?)null);

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.InvalidCredentials);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserInfoIsNull()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns(new GoogleTokenResponse { AccessToken = UserData.Token.AccessToken });

            this._googleServiceMock.GetUserInfoAsync(UserData.Token.AccessToken, default)
                .Returns((GoogleUserInfo?)null);

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.InvalidCredentials);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserDoesNotExist()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns(new GoogleTokenResponse { AccessToken = UserData.Token.AccessToken });

            this._googleServiceMock.GetUserInfoAsync(UserData.Token.AccessToken, default)
                .Returns(new GoogleUserInfo());

            this._userRepositoryMock.ExistByEmailAsync(UserData.Email, default).Returns(false);

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.InvalidCredentials);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenAuthenticationFails()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns(new GoogleTokenResponse { AccessToken = UserData.Token.AccessToken });

            var userInfo = new GoogleUserInfo { Email = UserData.Email.Value };
            this._googleServiceMock.GetUserInfoAsync(UserData.Token.AccessToken, default)
                .Returns(userInfo);

            this._userRepositoryMock.ExistByEmailAsync(new Email(userInfo.Email), default)
                .Returns(true);

            this._jwtServiceMock.AuthenticateWithGoogleAsync(userInfo, default)
                .Returns(Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials));

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.InvalidCredentials);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenAuthenticationThrows()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns(new GoogleTokenResponse { AccessToken = UserData.Token.AccessToken });

            var userInfo = new GoogleUserInfo { Email = UserData.Email.Value };
            this._googleServiceMock.GetUserInfoAsync(UserData.Token.AccessToken, default)
                .Returns(userInfo);

            this._userRepositoryMock.ExistByEmailAsync(new Email(userInfo.Email), default)
                .Returns(true);

            this._jwtServiceMock.AuthenticateWithGoogleAsync(userInfo, default)
                .Throws(new HttpRequestException("Google service exception"));

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.RegistrationFailed);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns(new GoogleTokenResponse { AccessToken = UserData.Token.AccessToken });

            var userInfo = new GoogleUserInfo { Email = UserData.Email.Value };
            this._googleServiceMock.GetUserInfoAsync(UserData.Token.AccessToken, default)
                .Returns(userInfo);

            this._userRepositoryMock.ExistByEmailAsync(new Email(userInfo.Email), default)
                .Returns(true);

            this._jwtServiceMock.AuthenticateWithGoogleAsync(userInfo, default)
                .Returns(UserData.Token);

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
