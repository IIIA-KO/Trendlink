using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.UnitTests.Users;
using Trendlink.Application.Users.Instagarm.LinkInstagram;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.UnitTests.Instagram
{
    public class LinkInstagramAccountTests
    {
        private static readonly LinkInstagramCommand Command = new LinkInstagramCommand(
            "valid-code"
        );

        private readonly IUserRepository _userRepositoryMock;
        private readonly IUserContext _userContextMock;
        private readonly IInstagramService _instagramServiceMock;
        private readonly IKeycloakService _keycloakServiceMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly LinkInstagramCommandHandler _handler;

        public LinkInstagramAccountTests()
        {
            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._userContextMock = Substitute.For<IUserContext>();
            this._instagramServiceMock = Substitute.For<IInstagramService>();
            this._keycloakServiceMock = Substitute.For<IKeycloakService>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new LinkInstagramCommandHandler(
                this._userRepositoryMock,
                this._userContextMock,
                this._instagramServiceMock,
                this._keycloakServiceMock,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenInstagramAccountAlreadyLinked()
        {
            // Arrange
            User user = UserData.Create();

            this._userRepositoryMock.GetByIdAsync(Arg.Any<UserId>(), default).Returns(user);

            this._keycloakServiceMock.IsExternalIdentityProviderAccountLinkedAsync(
                Arg.Any<string>(),
                "instagram",
                default
            )
                .Returns(true);

            // Act
            Result result = await this._handler.Handle(Command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(InstagramAccountErrors.InstagramAccountAlreadyLinked);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenFacebookTokenIsNull()
        {
            // Arrange
            User user = UserData.Create();

            this._userRepositoryMock.GetByIdAsync(Arg.Any<UserId>(), default).Returns(user);

            this._keycloakServiceMock.IsExternalIdentityProviderAccountLinkedAsync(
                Arg.Any<string>(),
                "instagram",
                default
            )
                .Returns(false);

            this._instagramServiceMock.GetAccessTokenAsync(Arg.Any<string>(), default)
                .Returns((FacebookTokenResponse?)null);

            // Act
            Result result = await this._handler.Handle(Command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.InvalidCredentials);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenInstagramUserInfoRequestFails()
        {
            // Arrange
            User user = UserData.Create();

            this._userRepositoryMock.GetByIdAsync(Arg.Any<UserId>(), default).Returns(user);

            this._keycloakServiceMock.IsExternalIdentityProviderAccountLinkedAsync(
                Arg.Any<string>(),
                "instagram",
                default
            )
                .Returns(false);

            var facebookToken = new FacebookTokenResponse
            {
                AccessToken = "access-token",
                ExpiresAtUtc = DateTime.UtcNow.AddHours(1)
            };

            this._instagramServiceMock.GetAccessTokenAsync(Arg.Any<string>(), default)
                .Returns(facebookToken);

            var userInfoResult = Result.Failure<InstagramUserInfo>(UserErrors.InvalidCredentials);

            this._instagramServiceMock.GetInstagramAccountAsync(
                user.Id,
                facebookToken.AccessToken,
                default
            )
                .Returns(InstagramAccountData.Create());

            // Act
            Result result = await this._handler.Handle(Command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(userInfoResult.Error);
        }
    }
}
