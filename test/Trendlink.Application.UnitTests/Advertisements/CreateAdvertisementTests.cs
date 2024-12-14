using System.Reflection;
using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Advertisements.CreateAdvertisement;
using Trendlink.Application.UnitTests.Conditions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Common;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.Advertisements;

namespace Trendlink.Application.UnitTests.Advertisements
{
    public class CreateAdvertisementTests
    {
        private static readonly CreateAdvertisementCommand Command = new CreateAdvertisementCommand(
            AdvertisementData.Name,
            AdvertisementData.Price,
            AdvertisementData.Description
        );

        private readonly IAdvertisementRepository _advertisementRepositoryMock;
        private readonly IUserContext _userContextMock;
        private readonly IConditionRepository _conditionRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly CreateAdvertisementCommandHandler _handler;

        public CreateAdvertisementTests()
        {
            this._advertisementRepositoryMock = Substitute.For<IAdvertisementRepository>();
            this._userContextMock = Substitute.For<IUserContext>();
            this._conditionRepositoryMock = Substitute.For<IConditionRepository>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new CreateAdvertisementCommandHandler(
                this._advertisementRepositoryMock,
                this._userContextMock,
                this._conditionRepositoryMock,
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
            Result<AdvertisementId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ConditionErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenAdvertisementCreationFails()
        {
            // Arrange
            this._userContextMock.UserId.Returns(ConditionData.UserId);

            this._conditionRepositoryMock.GetByUserIdWithAdvertisementAsync(ConditionData.UserId)
                .Returns(ConditionData.Create());

            var invalidCommand = new CreateAdvertisementCommand(
                new Name(string.Empty),
                AdvertisementData.Price,
                AdvertisementData.Description
            );

            // Act
            Result<AdvertisementId> result = await this._handler.Handle(invalidCommand, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AdvertisementErrors.Invalid);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenAdvertisementAlreadyExists()
        {
            // Arrange
            Advertisement existingAdvertisement = AdvertisementData.Create();

            Condition condition = ConditionData.Create();

            FieldInfo? advertisementsField = typeof(Condition).GetField(
                "_advertisements",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            if (advertisementsField is not null)
            {
                var advertisements = (List<Advertisement>)advertisementsField.GetValue(condition);
                advertisements!.Add(existingAdvertisement);
            }

            this._userContextMock.UserId.Returns(ConditionData.UserId);

            this._conditionRepositoryMock.GetByUserIdWithAdvertisementAsync(ConditionData.UserId)
                .Returns(condition);

            // Act
            Result<AdvertisementId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AdvertisementErrors.Duplicate);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            Condition condition = ConditionData.Create();

            this._userContextMock.UserId.Returns(ConditionData.UserId);

            this._conditionRepositoryMock.GetByUserIdWithAdvertisementAsync(ConditionData.UserId)
                .Returns(condition);

            // Act
            Result<AdvertisementId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
