using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Calendar.UblockDate;
using Trendlink.Application.UnitTests.Cooperations;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Cooperations.BlockedDates;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.UnitTests.Calendar
{
    public class UnblockDateTests
    {
        private static readonly UnblockDateCommand Command =
            new(DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)));

        private readonly IBlockedDateRepository _blockedDateRepositoryMock;
        private readonly IUserContext _userContextMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly UnblockDateCommandHandler _handler;

        public UnblockDateTests()
        {
            this._blockedDateRepositoryMock = Substitute.For<IBlockedDateRepository>();
            this._userContextMock = Substitute.For<IUserContext>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.UtcNow.Returns(CooperationData.UtcNow);

            this._handler = new UnblockDateCommandHandler(
                this._blockedDateRepositoryMock,
                this._userContextMock,
                dateTimeProvider,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenDateIsInPast()
        {
            // Arrange
            var command = new UnblockDateCommand(
                DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1))
            );

            // Act
            Result result = await this._handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(BlockedDateErrors.PastDate);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenBlockedDateIsNotFound()
        {
            // Arrange
            this._userContextMock.UserId.Returns(UserId.New());

            this._blockedDateRepositoryMock.GetByDateAndUserIdAsync(
                Command.Date,
                this._userContextMock.UserId,
                default
            )
                .Returns((BlockedDate?)null);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(BlockedDateErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUserUnauthorized()
        {
            // Arrange
            var blockedDate = new BlockedDate(UserId.New(), Command.Date);
            this._userContextMock.UserId.Returns(UserId.New());

            this._blockedDateRepositoryMock.GetByDateAndUserIdAsync(
                Command.Date,
                this._userContextMock.UserId,
                default
            )
                .Returns(blockedDate);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotAuthorized);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            var userId = UserId.New();

            var blockedDate = new BlockedDate(userId, Command.Date);

            this._userContextMock.UserId.Returns(userId);

            this._blockedDateRepositoryMock.GetByDateAndUserIdAsync(Command.Date, userId, default)
                .Returns(blockedDate);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this._blockedDateRepositoryMock.Received(1).Remove(blockedDate);
            await this._unitOfWorkMock.Received(1).SaveChangesAsync(default);
        }
    }
}
