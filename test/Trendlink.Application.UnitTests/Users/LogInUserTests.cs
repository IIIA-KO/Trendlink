using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Users.LogInUser;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.UnitTests.Users
{
    public class LogInUserTests
    {
        public static readonly LogInUserCommand Command = new(UserData.Email, UserData.Password);

        private readonly IUserRepository _userRepositoryMock;
        private readonly IKeycloakService _jwtServicMock;

        private readonly LogInUserCommandHandler _handler;

        public LogInUserTests()
        {
            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._jwtServicMock = Substitute.For<IKeycloakService>();

            this._handler = new LogInUserCommandHandler(
                this._userRepositoryMock,
                this._jwtServicMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserIsNull()
        {
            // Arrange
            this._userRepositoryMock.ExistByEmailAsync(Arg.Any<Email>(), default).Returns(false);

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnAccessTokenResponse_WhenAuthenticationIsSuccessful()
        {
            // Arrange
            this._userRepositoryMock.ExistByEmailAsync(Arg.Any<Email>(), default).Returns(true);

            this._jwtServicMock.GetAccessTokenAsync(
                Command.Email,
                Command.Password,
                Arg.Any<CancellationToken>()
            )
                .Returns(Result.Success(UserData.Token));

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(UserData.Token);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenAuthenticationFails()
        {
            // Arrange
            this._userRepositoryMock.ExistByEmailAsync(Arg.Any<Email>(), default).Returns(true);

            this._jwtServicMock.GetAccessTokenAsync(
                Command.Email,
                Command.Password,
                Arg.Any<CancellationToken>()
            )
                .Returns(Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials));

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.InvalidCredentials);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenKeycloakServiceThrowsException()
        {
            // Arrange
            this._userRepositoryMock.ExistByEmailAsync(Arg.Any<Email>(), default).Returns(true);

            this._jwtServicMock.GetAccessTokenAsync(
                Command.Email,
                Command.Password,
                Arg.Any<CancellationToken>()
            )
                .Returns(
                    Result.Failure<AccessTokenResponse>(
                        new Error(
                            "Keycloak.AuthenticationFailed",
                            "Failed to acquire access token due to authentication failure"
                        )
                    )
                );

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.InvalidCredentials);
        }
    }
}
