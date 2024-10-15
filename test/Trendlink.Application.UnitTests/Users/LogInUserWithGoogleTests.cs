using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Accounts.LogIn;
using Trendlink.Application.Accounts.LoginWithGoogle;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.VerificationTokens;

namespace Trendlink.Application.UnitTests.Users
{
    public class LogInUserWithGoogleTests
    {
        private static readonly LoginWithGoogleCommand Command = new(UserData.Code);

        private readonly IGoogleService _googleServiceMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IKeycloakService _keycloakServiceMock;

        private readonly LoginWithGoogleCommandHandler _handler;

        public LogInUserWithGoogleTests()
        {
            this._googleServiceMock = Substitute.For<IGoogleService>();
            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._keycloakServiceMock = Substitute.For<IKeycloakService>();

            this._handler = new LoginWithGoogleCommandHandler(
                this._googleServiceMock,
                this._userRepositoryMock,
                this._keycloakServiceMock
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

            this._keycloakServiceMock.AuthenticateWithGoogleAsync(userInfo, default)
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

            User? verifiedUser = UserData.Create();
            verifiedUser.VerifyEmail(
                new EmailVerificationToken(verifiedUser.Id, DateTime.Now, DateTime.Now.AddDays(1))
            );
            this._userRepositoryMock.GetByEmailAsync(new Email(userInfo.Email), default)
                .Returns(verifiedUser);

            this._keycloakServiceMock.AuthenticateWithGoogleAsync(userInfo, default)
                .Throws(new HttpRequestException("Google service exception"));

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.RegistrationFailed);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenEmailIsNotVerified()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns(new GoogleTokenResponse { AccessToken = UserData.Token.AccessToken });

            var userInfo = new GoogleUserInfo { Email = UserData.Email.Value };
            this._googleServiceMock.GetUserInfoAsync(UserData.Token.AccessToken, default)
                .Returns(userInfo);

            this._userRepositoryMock.GetByEmailAsync(new Email(userInfo.Email), default)
                .Returns(UserData.Create());

            this._keycloakServiceMock.AuthenticateWithGoogleAsync(userInfo, default)
                .Returns(UserData.Token);

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(EmailVerificationTokenErrors.EmailNotVerified);
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

            User? verifiedUser = UserData.Create();
            verifiedUser.VerifyEmail(
                new EmailVerificationToken(verifiedUser.Id, DateTime.Now, DateTime.Now.AddDays(1))
            );
            this._userRepositoryMock.GetByEmailAsync(new Email(userInfo.Email), default)
                .Returns(verifiedUser);

            this._keycloakServiceMock.AuthenticateWithGoogleAsync(userInfo, default)
                .Returns(UserData.Token);

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
