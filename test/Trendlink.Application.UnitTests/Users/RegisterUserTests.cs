using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Users.RegisterUser;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.UnitTests.Users
{
    public class RegisterUserTests
    {
        private static readonly RegisterUserCommand Command =
            new(
                UserData.FirstName,
                UserData.LastName,
                UserData.BirthDate,
                UserData.Email,
                UserData.PhoneNumber,
                UserData.Password,
                UserData.State.Id
            );

        private readonly RegisterUserCommandHandler _handler;

        private readonly IAuthenticationService _authenticationServiceMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IStateRepository _stateRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        public RegisterUserTests()
        {
            this._authenticationServiceMock = Substitute.For<IAuthenticationService>();
            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._stateRepositoryMock = Substitute.For<IStateRepository>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new RegisterUserCommandHandler(
                this._authenticationServiceMock,
                this._userRepositoryMock,
                this._stateRepositoryMock,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFaulire_When_UserExists()
        {
            // Arrange
            this._userRepositoryMock.ExistByEmailAsync(Command.Email, default).Returns(true);

            // Act
            Result<UserId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.DuplicateEmail);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_StateDoesNotExist()
        {
            // Arrange
            this._userRepositoryMock.ExistByEmailAsync(Command.Email, default).Returns(false);

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, Arg.Any<CancellationToken>())
                .Returns(false);

            // Act
            Result<UserId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(StateErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_UserCreationFails()
        {
            // Arrange
            this._userRepositoryMock.ExistByEmailAsync(Command.Email, default).Returns(false);

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, Arg.Any<CancellationToken>())
                .Returns(true);

            // Act
            var invalidCommand = new RegisterUserCommand(
                new FirstName(string.Empty),
                UserData.LastName,
                UserData.BirthDate,
                UserData.Email,
                UserData.PhoneNumber,
                UserData.Password,
                UserData.State.Id
            );
            Result<UserId> result = await this._handler.Handle(invalidCommand, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.InvalidCredentials);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_AuthenticationThrows()
        {
            // Arrange
            this._userRepositoryMock.ExistByEmailAsync(Command.Email, default).Returns(false);

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, Arg.Any<CancellationToken>())
                .Returns(true);

            this._authenticationServiceMock.RegisterAsync(
                Arg.Any<User>(),
                Command.Password,
                Arg.Any<CancellationToken>()
            )
                .Throws(new HttpRequestException());

            // Act
            Result<UserId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.RegistrationFailed);
        }

        [Fact]
        public async Task Handle_Should_SuccessfullyRegisterUser()
        {
            // Arrange
            this._userRepositoryMock.ExistByEmailAsync(Command.Email, default).Returns(false);

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, Arg.Any<CancellationToken>())
                .Returns(true);

            this._authenticationServiceMock.RegisterAsync(
                Arg.Any<User>(),
                Command.Password,
                Arg.Any<CancellationToken>()
            )
                .Returns("IdentityId");

            // Act
            Result<UserId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
