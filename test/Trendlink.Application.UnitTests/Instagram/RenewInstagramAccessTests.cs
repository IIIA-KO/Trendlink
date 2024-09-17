using System.Reflection;
using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.UnitTests.Users;
using Trendlink.Application.Users.Instagarm.RenewInstagramAccess;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;
using Trendlink.Domain.Users.Token;

namespace Trendlink.Application.UnitTests.Instagram
{
    public class RenewInstagramAccessTests
    {
        private static readonly RenewInstagramAccessCommand Command = new("valid-code");

        private readonly IUserContext _userContextMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IKeycloakService _keycloakServiceMock;
        private readonly IInstagramService _instagramServiceMock;
        private readonly IUserTokenRepository _userTokenRepositoryMock;
        private readonly IInstagramAccountRepository _instagramAccountRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly RenewInstagramAccessCommandHandler _handler;

        public RenewInstagramAccessTests()
        {
            this._userContextMock = Substitute.For<IUserContext>();
            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._keycloakServiceMock = Substitute.For<IKeycloakService>();
            this._instagramServiceMock = Substitute.For<IInstagramService>();
            this._userTokenRepositoryMock = Substitute.For<IUserTokenRepository>();
            this._instagramAccountRepositoryMock = Substitute.For<IInstagramAccountRepository>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new RenewInstagramAccessCommandHandler(
                this._userContextMock,
                this._userRepositoryMock,
                this._keycloakServiceMock,
                this._instagramServiceMock,
                this._userTokenRepositoryMock,
                this._instagramAccountRepositoryMock,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenInstagramNotLinked()
        {
            // Arrange
            User user = UserData.Create(); // Тестові дані

            this._userContextMock.UserId.Returns(user.Id);
            this._userRepositoryMock.GetByIdWithInstagramAccountAsync(user.Id, default)
                .Returns(user);

            this._keycloakServiceMock.IsExternalIdentityProviderAccountLinkedAsync(
                user.IdentityId,
                "instagram",
                default
            )
                .Returns(false);

            // Act
            Result result = await this._handler.Handle(Command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(InstagramAccountErrors.InstagramAccountNotLinked);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenFacebookTokenIsNull()
        {
            // Arrange
            User user = UserData.Create();
            this._userContextMock.UserId.Returns(user.Id);
            this._userRepositoryMock.GetByIdWithInstagramAccountAsync(user.Id, default)
                .Returns(user);

            this._keycloakServiceMock.IsExternalIdentityProviderAccountLinkedAsync(
                user.IdentityId,
                "instagram",
                default
            )
                .Returns(true);

            this._instagramServiceMock.RenewAccessTokenAsync(Arg.Any<string>(), default)
                .Returns((FacebookTokenResponse)null);

            // Act
            Result result = await this._handler.Handle(Command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.InvalidCredentials);
        }
    }
}
