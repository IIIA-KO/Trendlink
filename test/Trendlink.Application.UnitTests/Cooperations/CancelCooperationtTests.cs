using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Cooperations.CancelCooperation;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Cooperations;

namespace Trendlink.Application.UnitTests.Cooperations
{
    public class CancelCooperationtTests : CooperationBaseTest
    {
        public static readonly CancelCooperationCommand Command = new(CooperationData.Create().Id);

        private readonly ICooperationRepository _cooperationRepositoryMock;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly CancelCooperationCommandHandler _handler;

        public CancelCooperationtTests()
        {
            this._cooperationRepositoryMock = Substitute.For<ICooperationRepository>();
            this._dateTimeProvider = Substitute.For<IDateTimeProvider>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new CancelCooperationCommandHandler(
                this._cooperationRepositoryMock,
                this._dateTimeProvider,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenCooperationIsNull()
        {
            // Arrange
            this._cooperationRepositoryMock.GetByIdAsync(Command.CooperationId, default)
                .Returns((Cooperation?)null);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenConfirmationFails()
        {
            // Arrange
            Cooperation cooperation = this.CreatePendingCooperation();

            this._cooperationRepositoryMock.GetByIdAsync(Command.CooperationId, default)
                .Returns(cooperation);

            this._dateTimeProvider.UtcNow.Returns(CooperationData.UtcNow);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.NotConfirmed);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenCooperationIsAlreadyStarted()
        {
            // Arrange
            Cooperation cooperation = this.CreateConfirmedCooperation();

            this._cooperationRepositoryMock.GetByIdAsync(Command.CooperationId, default)
                .Returns(cooperation);

            this._dateTimeProvider.UtcNow.Returns(CooperationData.UtcNow.AddDays(10));

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.AlreadyStarted);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            Cooperation cooperation = this.CreateConfirmedCooperation();

            this._cooperationRepositoryMock.GetByIdAsync(Command.CooperationId, default)
                .Returns(cooperation);

            this._dateTimeProvider.UtcNow.Returns(CooperationData.UtcNow);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
