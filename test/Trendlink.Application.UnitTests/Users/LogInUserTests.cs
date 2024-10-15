using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Accounts.LogIn;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.VerificationTokens;

namespace Trendlink.Application.UnitTests.Users
{
    public class LogInUserTests
    {
        public static readonly LogInCommand Command = new(UserData.Email, UserData.Password);

        private readonly IUserRepository _userRepositoryMock;
        private readonly IKeycloakService _keycloakServiceMock;

        private readonly LogInCommandHandler _handler;

        public LogInUserTests()
        {
            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._keycloakServiceMock = Substitute.For<IKeycloakService>();

            this._handler = new LogInCommandHandler(
                this._userRepositoryMock,
                this._keycloakServiceMock
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
        {
            // Arrange
            this._userRepositoryMock.GetByEmailAsync(Command.Email, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<User?>(null));

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(
                Command,
                CancellationToken.None
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotFound);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenEmailNotVerified()
        {
            // Arrange
            User unverifiedUser = UserData.Create();
            this._userRepositoryMock.GetByEmailAsync(Command.Email, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<User?>(unverifiedUser));

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(
                Command,
                CancellationToken.None
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(EmailVerificationTokenErrors.EmailNotVerified);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenInvalidCredentials()
        {
            // Arrange
            User verifiedUser = UserData.Create();

            this._userRepositoryMock.GetByEmailAsync(Command.Email, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<User?>(verifiedUser));

            verifiedUser.VerifyEmail(
                new EmailVerificationToken(verifiedUser.Id, DateTime.Now, DateTime.Now.AddDays(1))
            );

            this._keycloakServiceMock.GetAccessTokenAsync(
                Command.Email,
                Command.Password,
                Arg.Any<CancellationToken>()
            )
                .Returns(Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials));

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(
                Command,
                CancellationToken.None
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.InvalidCredentials);
        }

        [Fact]
        public async Task Handle_ShouldReturnAccessToken_WhenCredentialsAreValid()
        {
            // Arrange
            User verifiedUser = UserData.Create();
            var tokenResponse = new AccessTokenResponse("valid_token", "refresh_token", 900);

            verifiedUser.VerifyEmail(
                new EmailVerificationToken(verifiedUser.Id, DateTime.Now, DateTime.Now.AddDays(1))
            );

            this._userRepositoryMock.GetByEmailAsync(Command.Email, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<User?>(verifiedUser));

            this._keycloakServiceMock.GetAccessTokenAsync(
                Command.Email,
                Command.Password,
                Arg.Any<CancellationToken>()
            )
                .Returns(Result.Success(tokenResponse));

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(
                Command,
                CancellationToken.None
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(tokenResponse);
        }
    }
}
