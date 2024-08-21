using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Cooperations.CompleteCooperation;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.UnitTests.Cooperations
{
    public class CompleteCooperationTests : CooperationBaseTest
    {
        public static readonly CompleteCooperationCommand Command =
            new(CooperationData.Create().Id);

        private readonly ICooperationRepository _cooperationRepositoryMock;
        private readonly IUserContext _userContextMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly CompleteCooperationCommandHandler _handler;

        public CompleteCooperationTests()
        {
            this._cooperationRepositoryMock = Substitute.For<ICooperationRepository>();
            this._userContextMock = Substitute.For<IUserContext>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.UtcNow.Returns(CooperationData.UtcNow);

            this._handler = new CompleteCooperationCommandHandler(
                this._cooperationRepositoryMock,
                this._userContextMock,
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
        public async Task Handle_Should_ReturnFailure_WhenUserIsNotAuthorized()
        {
            // Arrange
            Cooperation cooperation = CreatePendingCooperation(CooperationData.ScheduledOnUtc);

            this._cooperationRepositoryMock.GetByIdAsync(Command.CooperationId, default)
                .Returns(cooperation);

            this._userContextMock.UserId.Returns(UserId.New());

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotAuthorized);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenCompletionFails()
        {
            // Arrange
            Cooperation cooperation = CreatePendingCooperation(CooperationData.ScheduledOnUtc);

            this._cooperationRepositoryMock.GetByIdAsync(Command.CooperationId, default)
                .Returns(cooperation);

            this._userContextMock.UserId.Returns(cooperation.BuyerId);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.NotDone);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            Cooperation cooperation = CreateDoneCooperation();

            this._cooperationRepositoryMock.GetByIdAsync(Command.CooperationId, default)
                .Returns(cooperation);

            this._userContextMock.UserId.Returns(cooperation.BuyerId);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
