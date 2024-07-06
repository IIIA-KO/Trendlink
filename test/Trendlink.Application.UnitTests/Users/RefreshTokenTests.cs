using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Users.LogInUser;
using Trendlink.Application.Users.RefreshToken;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Users
{
    public class RefreshTokenTests
    {
        public static readonly RefreshTokenCommand Command = new(UserData.Token.RefreshToken);

        private readonly IJwtService _jwtServiceMock;

        private readonly RefreshTokenCommandHandler _handler;

        public RefreshTokenTests()
        {
            this._jwtServiceMock = Substitute.For<IJwtService>();

            this._handler = new RefreshTokenCommandHandler(this._jwtServiceMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnAccessTokenResponse_WhenRefreshIsSuccessful()
        {
            // Arrange
            this._jwtServiceMock.RefreshTokenAsync(
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
            this._jwtServiceMock.RefreshTokenAsync(
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
        public async Task Handle_Should_ReturnFailure_WhenJwtServiceThrowsException()
        {
            // Arrange
            this._jwtServiceMock.RefreshTokenAsync(
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
