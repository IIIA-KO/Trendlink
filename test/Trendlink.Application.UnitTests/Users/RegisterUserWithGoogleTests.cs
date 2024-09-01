using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Exceptions;
using Trendlink.Application.Users.LogInUser;
using Trendlink.Application.Users.RegisterUserWithGoogle;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.UnitTests.Users
{
    public class RegisterUserWithGoogleTests
    {
        private static readonly RegisterUserWithGoogleCommand Command =
            new RegisterUserWithGoogleCommand(
                UserData.Code,
                UserData.BirthDate,
                UserData.PhoneNumber,
                UserData.State.Id
            );

        private readonly IGoogleService _googleServiceMock;
        private readonly IJwtService _jwtServiceMock;
        private readonly IAuthenticationService _authenticationServiceMock;

        private readonly IUserRepository _userRepositoryMock;
        private readonly IStateRepository _stateRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly RegisterUserWithGoogleCommandHandler _handler;

        public RegisterUserWithGoogleTests()
        {
            this._googleServiceMock = Substitute.For<IGoogleService>();
            this._jwtServiceMock = Substitute.For<IJwtService>();
            this._authenticationServiceMock = Substitute.For<IAuthenticationService>();

            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._stateRepositoryMock = Substitute.For<IStateRepository>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new RegisterUserWithGoogleCommandHandler(
                this._googleServiceMock,
                this._jwtServiceMock,
                this._authenticationServiceMock,
                this._userRepositoryMock,
                this._stateRepositoryMock,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenAccessTokenIsNull()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns((string?)null);

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
                .Returns(UserData.Token.AccessToken);

            this._googleServiceMock.GetUserInfoAsync(UserData.Token.AccessToken, default)
                .Returns((UserInfo?)null);

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.InvalidCredentials);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenStateDoesNotExist()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns(UserData.Token.AccessToken);

            this._googleServiceMock.GetUserInfoAsync(UserData.Token.AccessToken, default)
                .Returns(new UserInfo());

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, default).Returns(false);

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(StateErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserExistsInDatabase()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns(UserData.Token.AccessToken);

            var userInfo = new UserInfo() { Email = UserData.Email.Value };
            this._googleServiceMock.GetUserInfoAsync(UserData.Token.AccessToken, default)
                .Returns(userInfo);

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, default).Returns(true);

            this._userRepositoryMock.ExistByEmailAsync(new Email(userInfo.Email), default)
                .Returns(true);

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.DuplicateEmail);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserExistsInKeycloak()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns(UserData.Token.AccessToken);

            var userInfo = new UserInfo() { Email = UserData.Email.Value };
            this._googleServiceMock.GetUserInfoAsync(UserData.Token.AccessToken, default)
                .Returns(userInfo);

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, default).Returns(true);

            this._userRepositoryMock.ExistByEmailAsync(new Email(userInfo.Email), default)
                .Returns(false);

            this._jwtServiceMock.CheckUserExistsInKeycloak(userInfo.Email, default).Returns(true);

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.DuplicateEmail);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserCreationFails()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns(UserData.Token.AccessToken);

            var userInfo = new UserInfo()
            {
                Email = UserData.Email.Value,
                Name = UserData.FirstName.Value,
                GivenName = UserData.LastName.Value
            };
            this._googleServiceMock.GetUserInfoAsync(UserData.Token.AccessToken, default)
                .Returns(userInfo);

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, default).Returns(true);

            this._userRepositoryMock.ExistByEmailAsync(new Email(userInfo.Email), default)
                .Returns(false);

            this._jwtServiceMock.CheckUserExistsInKeycloak(userInfo.Email, default).Returns(false);

            var invalidCommand = new RegisterUserWithGoogleCommand(
                UserData.Code,
                DateOnly.FromDateTime(DateTime.Now),
                UserData.PhoneNumber,
                UserData.State.Id
            );

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(
                invalidCommand,
                default
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.Underage);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhendRegisterThrows()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns(UserData.Token.AccessToken);

            var userInfo = new UserInfo()
            {
                Email = UserData.Email.Value,
                Name = UserData.FirstName.Value,
                GivenName = UserData.LastName.Value
            };
            this._googleServiceMock.GetUserInfoAsync(UserData.Token.AccessToken, default)
                .Returns(userInfo);

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, default).Returns(true);

            this._userRepositoryMock.ExistByEmailAsync(new Email(userInfo.Email), default)
                .Returns(false);

            this._jwtServiceMock.CheckUserExistsInKeycloak(userInfo.Email, default).Returns(false);

            this._authenticationServiceMock.RegisterAsync(Arg.Any<User>(), userInfo.Email, default)
                .Throws(new HttpRequestException("Google service exception"));

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.RegistrationFailed);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUnitOfWorkThrows()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns(UserData.Token.AccessToken);

            var userInfo = new UserInfo()
            {
                Email = UserData.Email.Value,
                Name = UserData.FirstName.Value,
                GivenName = UserData.LastName.Value
            };
            this._googleServiceMock.GetUserInfoAsync(UserData.Token.AccessToken, default)
                .Returns(userInfo);

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, default).Returns(true);

            this._userRepositoryMock.ExistByEmailAsync(new Email(userInfo.Email), default)
                .Returns(false);

            this._jwtServiceMock.CheckUserExistsInKeycloak(userInfo.Email, default).Returns(false);

            this._authenticationServiceMock.RegisterAsync(Arg.Any<User>(), userInfo.Email, default)
                .Returns(UserData.Token.AccessToken);

            this._unitOfWorkMock.SaveChangesAsync(default)
                .Throws(new ConcurrencyException("Database exception", new Exception()));

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.RegistrationFailed);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenAuthenticationThrows()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns(UserData.Token.AccessToken);

            var userInfo = new UserInfo()
            {
                Email = UserData.Email.Value,
                Name = UserData.FirstName.Value,
                GivenName = UserData.LastName.Value
            };
            this._googleServiceMock.GetUserInfoAsync(UserData.Token.AccessToken, default)
                .Returns(userInfo);

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, default).Returns(true);

            this._userRepositoryMock.ExistByEmailAsync(new Email(userInfo.Email), default)
                .Returns(false);

            this._jwtServiceMock.CheckUserExistsInKeycloak(userInfo.Email, default).Returns(false);

            this._authenticationServiceMock.RegisterAsync(Arg.Any<User>(), userInfo.Email, default)
                .Returns(UserData.Token.AccessToken);

            this._jwtServiceMock.AuthenticateWithGoogleAsync(userInfo, default)
                .Throws(new HttpRequestException("Google service exception"));

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.RegistrationFailed);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            this._googleServiceMock.GetAccessTokenAsync(Command.Code, default)
                .Returns(UserData.Token.AccessToken);

            var userInfo = new UserInfo()
            {
                Email = UserData.Email.Value,
                Name = UserData.FirstName.Value,
                GivenName = UserData.LastName.Value
            };
            this._googleServiceMock.GetUserInfoAsync(UserData.Token.AccessToken, default)
                .Returns(userInfo);

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, default).Returns(true);

            this._userRepositoryMock.ExistByEmailAsync(new Email(userInfo.Email), default)
                .Returns(false);

            this._jwtServiceMock.CheckUserExistsInKeycloak(userInfo.Email, default).Returns(false);

            this._authenticationServiceMock.RegisterAsync(Arg.Any<User>(), userInfo.Email, default)
                .Returns(UserData.Token.AccessToken);

            this._jwtServiceMock.AuthenticateWithGoogleAsync(userInfo, default)
                .Returns(UserData.Token);

            // Act
            Result<AccessTokenResponse> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
