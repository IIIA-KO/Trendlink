using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Advertisements.DeleteAdvertisement;
using Trendlink.Application.UnitTests.Conditions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.Advertisements;

namespace Trendlink.Application.UnitTests.Advertisements
{
    public class DeleteAdvertisementTests
    {
        private static readonly DeleteAdvertisementCommand Command =
            new(AdvertisementData.AdvertisementId);

        private readonly IAdvertisementRepository _advertisementRepositoryMock;
        private readonly IConditionRepository _conditionRepositoryMock;
        private readonly ICooperationRepository _cooperationRepositoryMock;
        private readonly IUserContext _userContextMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly DeleteAdvertisementCommandHandler _handler;

        public DeleteAdvertisementTests()
        {
            this._advertisementRepositoryMock = Substitute.For<IAdvertisementRepository>();
            this._conditionRepositoryMock = Substitute.For<IConditionRepository>();
            this._cooperationRepositoryMock = Substitute.For<ICooperationRepository>();
            this._userContextMock = Substitute.For<IUserContext>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new DeleteAdvertisementCommandHandler(
                this._advertisementRepositoryMock,
                this._conditionRepositoryMock,
                this._cooperationRepositoryMock,
                this._userContextMock,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenConditionIsNull()
        {
            // Arrange
            this._userContextMock.UserId.Returns(ConditionData.UserId);

            this._conditionRepositoryMock.GetByUserIdWithAdvertisementAsync(ConditionData.UserId)
                .Returns((Condition?)null);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ConditionErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenAdvertisementIsNull()
        {
            // Arrange
            this._userContextMock.UserId.Returns(ConditionData.UserId);

            this._conditionRepositoryMock.GetByUserIdWithAdvertisementAsync(ConditionData.UserId)
                .Returns(ConditionData.Create());

            this._advertisementRepositoryMock.GetByIdAsync(Command.AdvertisementId, default)
                .Returns((Advertisement?)null);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AdvertisementErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenAvertisementHasActiveCooperations()
        {
            // Arrange
            this._userContextMock.UserId.Returns(ConditionData.UserId);

            this._conditionRepositoryMock.GetByUserIdWithAdvertisementAsync(ConditionData.UserId)
                .Returns(ConditionData.Create());

            this._advertisementRepositoryMock.GetByIdAsync(Command.AdvertisementId, default)
                .Returns(AdvertisementData.Create());

            this._cooperationRepositoryMock.HasActiveCooperationsForAdvertisement(
                Arg.Any<Advertisement>(),
                default
            )
                .Returns(true);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AdvertisementErrors.HasActiveCooperations);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            this._userContextMock.UserId.Returns(ConditionData.UserId);

            this._conditionRepositoryMock.GetByUserIdWithAdvertisementAsync(ConditionData.UserId)
                .Returns(ConditionData.Create());

            this._advertisementRepositoryMock.GetByIdAsync(Command.AdvertisementId, default)
                .Returns(AdvertisementData.Create());

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
