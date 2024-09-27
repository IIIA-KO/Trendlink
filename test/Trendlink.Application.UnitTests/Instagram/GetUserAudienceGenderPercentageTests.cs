using System.Reflection;
using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Instagarm.Audience.GetAudienceGenderPercentage;
using Trendlink.Application.UnitTests.Users;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;
using Trendlink.Domain.Users.Token;

namespace Trendlink.Application.UnitTests.Instagram
{
    public class GetUserAudienceGenderPercentageTests
    {
        private static readonly GetAudienceGenderPercentageQuery Query = new(UserId.New());

        private readonly IUserRepository _userRepositoryMock;
        private readonly IInstagramService _instagramServiceMock;
        private readonly IKeycloakService _keycloakServiceMock;

        private readonly GetAudienceGenderPercentageQueryHandler _handler;

        public GetUserAudienceGenderPercentageTests()
        {
            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._instagramServiceMock = Substitute.For<IInstagramService>();
            this._keycloakServiceMock = Substitute.For<IKeycloakService>();

            this._handler = new GetAudienceGenderPercentageQueryHandler(
                this._userRepositoryMock,
                this._instagramServiceMock,
                this._keycloakServiceMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserIsNull()
        {
            // Arrange
            this._userRepositoryMock.GetByIdWithInstagramAccountAndTokenAsync(
                Arg.Any<UserId>(),
                default
            )
                .Returns(Task.FromResult<User?>(null));

            // Act
            Result<AudienceGenderStatistics> result = await this._handler.Handle(
                Query,
                CancellationToken.None
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenInstagramAccountIsNotLinked()
        {
            // Arrange
            User user = UserData.Create();

            this._userRepositoryMock.GetByIdWithInstagramAccountAndTokenAsync(
                Arg.Any<UserId>(),
                default
            )
                .Returns(Task.FromResult<User?>(user));

            this._keycloakServiceMock.IsExternalIdentityProviderAccountLinkedAsync(
                user.IdentityId,
                "instagram",
                default
            )
                .Returns(Task.FromResult(false));

            // Act
            Result<AudienceGenderStatistics> result = await this._handler.Handle(
                Query,
                CancellationToken.None
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(InstagramAccountErrors.InstagramAccountNotLinked);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            var audienceData = new AudienceGenderStatistics(
                [
                    new() { Gender = "male", Percentage = 60 },
                    new() { Gender = "female", Percentage = 40 }
                ]
            );

            User user = UserData.Create();
            InstagramAccount instagramAccount = InstagramAccountData.Create();
            UserToken token = UserToken.Create(user.Id, "access-token", DateTimeOffset.Now).Value;

            PropertyInfo? instagramAccountProperty = typeof(User).GetProperty("InstagramAccount");
            instagramAccountProperty?.SetValue(user, instagramAccount);

            PropertyInfo? tokenProperty = typeof(User).GetProperty("Token");
            tokenProperty?.SetValue(user, token);

            this._userRepositoryMock.GetByIdWithInstagramAccountAndTokenAsync(
                Arg.Any<UserId>(),
                default
            )
                .Returns(Task.FromResult<User?>(user));

            this._keycloakServiceMock.IsExternalIdentityProviderAccountLinkedAsync(
                user.IdentityId,
                "instagram",
                default
            )
                .Returns(Task.FromResult(true));

            this._instagramServiceMock.GetAudienceGenderPercentage(
                user.Token!.AccessToken,
                user.InstagramAccount!.Metadata.Id,
                default
            )
                .Returns(audienceData);

            // Act
            Result<AudienceGenderStatistics> result = await this._handler.Handle(
                Query,
                CancellationToken.None
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(audienceData);
        }
    }
}
