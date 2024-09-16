using FluentAssertions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Domain.UnitTests.InstagramAccounts
{
    public class InstagramAccountTests
    {
        [Fact]
        public void Create_Should_RetunFailure_WhenFacebookPageIdIsInvalid()
        {
            // Act
            Result<InstagramAccount> result = InstagramAccount.Create(
                InstagramAccountData.UserId,
                InstagramAccountData.FacebookPageId with
                {
                    Value = string.Empty
                },
                InstagramAccountData.Metadata
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(InstagramAccountErrors.InvalidFacebookPageId);
        }

        [Fact]
        public void Create_Should_ReturnFailure_WhenMetadataIdIsInvalid()
        {
            // Act
            Result<InstagramAccount> result = InstagramAccount.Create(
                InstagramAccountData.UserId,
                InstagramAccountData.FacebookPageId,
                InstagramAccountData.Metadata with
                {
                    Id = string.Empty
                }
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(InstagramAccountErrors.InvalidId);
        }

        [Fact]
        public void Create_Should_ReturnSuccess()
        {
            // Act
            Result<InstagramAccount> result = InstagramAccount.Create(
                InstagramAccountData.UserId,
                InstagramAccountData.FacebookPageId,
                InstagramAccountData.Metadata
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.UserId.Should().Be(InstagramAccountData.UserId);
            result.Value.FacebookPageId.Should().Be(InstagramAccountData.FacebookPageId);
            result.Value.Metadata.Should().Be(InstagramAccountData.Metadata);
        }

        [Fact]
        public void Update_Should_Update_Metadata()
        {
            // Arrange
            var userId = new UserId(Guid.NewGuid());
            var facebookPageId = new FacebookPageId("1234567890");
            var oldMetadata = new Metadata("1", 1234567890, "old_user", 1000, 10);
            var newMetadata = new Metadata("2", 1234567890, "new_user", 2000, 20);

            InstagramAccount instagramAccount = InstagramAccount
                .Create(userId, facebookPageId, oldMetadata)
                .Value;
            InstagramAccount updatedInstagramAccount = InstagramAccount
                .Create(userId, facebookPageId, newMetadata)
                .Value;

            // Act
            instagramAccount.Update(updatedInstagramAccount);

            // Assert
            instagramAccount.Metadata.Should().Be(newMetadata);
        }
    }
}
