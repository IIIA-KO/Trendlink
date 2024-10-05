using FluentAssertions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.Token;

namespace Trendlink.Domain.UnitTests.Token
{
    public class UserTokenTests
    {
        [Fact]
        public void Create_Should_ReturnFailure_WhenAccessTokenIsInvalid()
        {
            // Act
            Result<UserToken> result = UserToken.Create(
                UserTokenData.UserId,
                string.Empty,
                UserTokenData.ExpiresIn
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserTokenErrors.AccessTokenInvalid);
        }

        [Fact]
        public void LastCheckedOnUtc_Should_Be_Null_By_Default()
        {
            // Act
            Result<UserToken> result = UserToken.Create(
                UserTokenData.UserId,
                UserTokenData.AccessToken,
                UserTokenData.ExpiresIn
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.LastCheckedOnUtc.Should().BeNull();
        }

        [Fact]
        public void Create_Should_ReturnSuccess()
        {
            // Act
            Result<UserToken> result = UserToken.Create(
                UserTokenData.UserId,
                UserTokenData.AccessToken,
                UserTokenData.ExpiresIn
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.UserId.Should().Be(UserTokenData.UserId);
            result.Value.AccessToken.Should().Be(UserTokenData.AccessToken);
            result.Value.ExpiresAtUtc.Should().Be(UserTokenData.ExpiresIn);
            result.Value.Id.Should().NotBe(Guid.Empty);
        }
    }
}
