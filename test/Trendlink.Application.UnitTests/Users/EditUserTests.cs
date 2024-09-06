using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Users.EditUser;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;
using Trendlink.Domain.Users.States;

namespace Trendlink.Application.UnitTests.Users
{
    public class EditUserTests
    {
        private static readonly EditUserCommand Command =
            new(
                UserId.New(),
                UserData.FirstName,
                UserData.LastName,
                UserData.BirthDate,
                UserData.State.Id,
                UserData.Bio,
                UserData.AccountCategory
            );

        private readonly EditUserCommandHandler _handler;

        private readonly IUserRepository _userRepositoryMock;
        private readonly IStateRepository _stateRepositoryMock;
        private readonly IUserContext _userContextMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        public EditUserTests()
        {
            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._stateRepositoryMock = Substitute.For<IStateRepository>();
            this._userContextMock = Substitute.For<IUserContext>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new EditUserCommandHandler(
                this._userRepositoryMock,
                this._stateRepositoryMock,
                this._userContextMock,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_UserIsNull()
        {
            // Arrange
            this._userRepositoryMock.GetByIdWithRolesAsync(
                Command.UserId,
                Arg.Any<CancellationToken>()
            )
                .Returns((User?)null);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_IdentityIdsDoNotMatchAndUserIsNotAdmin()
        {
            // Arrange
            User user = UserData.Create();
            user.SetIdentityId("456");

            this._userRepositoryMock.GetByIdWithRolesAsync(
                Command.UserId,
                Arg.Any<CancellationToken>()
            )
                .Returns(user);

            this._userContextMock.IdentityId.Returns("123");

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotAuthorized);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_When_UserIsAdminAndIdentityIdsDoNotMatch()
        {
            // Arrange
            User user = UserData.Create();
            user.SetIdentityId("123");

            user.AddRole(Role.Administrator);

            this._userRepositoryMock.GetByIdWithRolesAsync(
                Command.UserId,
                Arg.Any<CancellationToken>()
            )
                .Returns(user);

            this._userContextMock.IdentityId.Returns("456");

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, Arg.Any<CancellationToken>())
                .Returns(true);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_When_UserIsAdminnAndIdentityIdsDoMatch()
        {
            // Arrange
            User user = UserData.Create();
            user.SetIdentityId("123");

            user.AddRole(Role.Administrator);

            this._userRepositoryMock.GetByIdWithRolesAsync(
                Command.UserId,
                Arg.Any<CancellationToken>()
            )
                .Returns(user);

            this._userContextMock.IdentityId.Returns("123");

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, Arg.Any<CancellationToken>())
                .Returns(true);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_When_IdentityIdsDoMatch()
        {
            // Arrange
            User user = UserData.Create();
            user.SetIdentityId("123");

            this._userRepositoryMock.GetByIdWithRolesAsync(
                Command.UserId,
                Arg.Any<CancellationToken>()
            )
                .Returns(user);

            this._userContextMock.IdentityId.Returns("123");

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, Arg.Any<CancellationToken>())
                .Returns(true);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_StateDoesNotExist()
        {
            // Arrange
            User user = UserData.Create();
            user.SetIdentityId("123");

            this._userRepositoryMock.GetByIdWithRolesAsync(
                Command.UserId,
                Arg.Any<CancellationToken>()
            )
                .Returns(user);

            this._userContextMock.IdentityId.Returns("123");

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, Arg.Any<CancellationToken>())
                .Returns(false);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(StateErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_UserUpdateFails()
        {
            // Arrange
            User user = UserData.Create();
            user.SetIdentityId("123");

            this._userRepositoryMock.GetByIdWithRolesAsync(
                Command.UserId,
                Arg.Any<CancellationToken>()
            )
                .Returns(user);

            this._userContextMock.IdentityId.Returns("123");

            this._stateRepositoryMock.ExistsByIdAsync(Command.StateId, Arg.Any<CancellationToken>())
                .Returns(true);

            // Act
            var invalidCommand = new EditUserCommand(
                Command.UserId,
                UserData.FirstName,
                UserData.LastName,
                DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-(UserData.MinimumAge - 1))),
                UserData.State.Id,
                UserData.Bio,
                AccountCategory.Education
            );

            // Act
            Result result = await this._handler.Handle(invalidCommand, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.Underage);
        }
    }
}
