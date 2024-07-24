using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Cooperations.ConfirmCooperation;
using Trendlink.Application.Cooperations.MarkCooperationAsDone;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Cooperations;

namespace Trendlink.Application.UnitTests.Cooperations
{
    public class MarkCooperationAsDoneTests : CooperationBaseTest
    {
        public static readonly MarkCooperationAsDoneCommand Command =
            new(CooperationData.Create().Id);

        private readonly ICooperationRepository _cooperationRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly MarkCooperationAsDoneCommandHandler _handler;

        public MarkCooperationAsDoneTests()
        {
            this._cooperationRepositoryMock = Substitute.For<ICooperationRepository>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.UtcNow.Returns(CooperationData.UtcNow);

            this._handler = new MarkCooperationAsDoneCommandHandler(
                this._cooperationRepositoryMock,
                dateTimeProvider,
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
        public async Task Handle_Should_ReturnFailure_WhenMarkingFails()
        {
            // Arrange
            Cooperation cooperation = this.CreatePendingCooperation();

            this._cooperationRepositoryMock.GetByIdAsync(Command.CooperationId, default)
                .Returns(cooperation);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.NotConfirmed);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            Cooperation cooperation = this.CreateConfirmedCooperation();

            this._cooperationRepositoryMock.GetByIdAsync(Command.CooperationId, default)
                .Returns(cooperation);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
