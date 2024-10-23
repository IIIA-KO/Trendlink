using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Accounts.LogIn;
using Trendlink.Application.Accounts.RefreshToken;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Users
{
    public class RefreshTokenTests
    {
        public static readonly RefreshTokenCommand Command = new(UserData.Token.RefreshToken);

        private readonly IKeycloakService _keycloakServiceMock;

        private readonly RefreshTokenCommandHandler _handler;

        public RefreshTokenTests()
        {
            this._keycloakServiceMock = Substitute.For<IKeycloakService>();

            this._handler = new RefreshTokenCommandHandler(this._keycloakServiceMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnAccessTokenResponse_WhenRefreshIsSuccessful()
        {
            // Arrange
            this._keycloakServiceMock.RefreshTokenAsync(
                UserData.Token.RefreshToken,
                Arg.Any<CancellationToken>()
            )
                .Returns(Result.Success(UserData.Token));

            var command = new RefreshTokenCommand(UserData.Token.RefreshToken);

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(UserData.Token);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenRefreshFails()
        {
            // Arrange
            this._keycloakServiceMock.RefreshTokenAsync(
                Command.RefreshToken,
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
            this._keycloakServiceMock.RefreshTokenAsync(
                Command.RefreshToken,
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
