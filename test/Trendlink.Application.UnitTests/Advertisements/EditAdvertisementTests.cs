using FluentAssertions;
using Microsoft.VisualBasic;
using NSubstitute;
using System.Reflection;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Advertisements.EditAdvertisement;
using Trendlink.Application.UnitTests.Conditions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;

namespace Trendlink.Application.UnitTests.Advertisements
{
    public class EditAdvertisementTests
    {
        private static readonly EditAdvertisementCommand Command =
            new(
                AdvertisementData.AdvertisementId,
                AdvertisementData.Name,
                AdvertisementData.Price,
                AdvertisementData.Description
            );

        private readonly IAdvertisementRepository _advertisementRepositoryMock;
        private readonly IUserContext _userContextMock;
        private readonly IConditionRepository _conditionRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly EditAdvertisementCommandHandler _handler;

        public EditAdvertisementTests()
        {
            this._advertisementRepositoryMock = Substitute.For<IAdvertisementRepository>();
            this._userContextMock = Substitute.For<IUserContext>();
            this._conditionRepositoryMock = Substitute.For<IConditionRepository>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new EditAdvertisementCommandHandler(
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

            this._advertisementRepositoryMock.GetByIdAsync(Command.AdvertisementId, default)
                .Returns(AdvertisementData.Create());

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AdvertisementErrors.Duplicate);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUpdateFails()
        {
            // Arrange
            this._userContextMock.UserId.Returns(ConditionData.UserId);

            this._conditionRepositoryMock.GetByUserIdWithAdvertisementAsync(ConditionData.UserId)
                .Returns(ConditionData.Create());

            this._advertisementRepositoryMock.GetByIdAsync(Command.AdvertisementId, default)
                .Returns(AdvertisementData.Create());

            var invalidCommand = new EditAdvertisementCommand(
                AdvertisementData.AdvertisementId,
                AdvertisementData.Name,
                AdvertisementData.Price,
                null!
            );

            // Act
            Result result = await this._handler.Handle(invalidCommand, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AdvertisementErrors.Invalid);
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
