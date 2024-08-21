using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Calendar.BlockDate;
using Trendlink.Application.UnitTests.Cooperations;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Cooperations.BlockedDates;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.UnitTests.Calendar
{
    public class BlockDateTests
    {
        private static readonly BlockDateCommand Command =
            new(DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)));

        private readonly IBlockedDateRepository _blockedDateRepositoryMock;
        private readonly IUserContext _userContextMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly BlockDateCommandHandler _handler;

        public BlockDateTests()
        {
            this._blockedDateRepositoryMock = Substitute.For<IBlockedDateRepository>();
            this._userContextMock = Substitute.For<IUserContext>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.UtcNow.Returns(CooperationData.UtcNow);

            this._handler = new BlockDateCommandHandler(
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
            var command = new BlockDateCommand(DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)));

            // Act
            Result result = await this._handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(BlockedDateErrors.PastDate);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenDateIsAlreadyBlocked()
        {
            // Arrange
            this._userContextMock.UserId.Returns(UserId.New());

            this._blockedDateRepositoryMock.ExistsByDateAndUserIdAsync(
                Command.Date,
                this._userContextMock.UserId,
                default
            )
                .Returns(true);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(BlockedDateErrors.AlreadyBlocked);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            this._userContextMock.UserId.Returns(UserId.New());

            this._blockedDateRepositoryMock.ExistsByDateAndUserIdAsync(
                Command.Date,
                this._userContextMock.UserId,
                default
            )
                .Returns(false);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            await this._unitOfWorkMock.Received(1).SaveChangesAsync(default);
        }
    }
}
