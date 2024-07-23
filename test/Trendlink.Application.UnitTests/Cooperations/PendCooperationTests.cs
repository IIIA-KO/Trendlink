using System.Reflection;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Cooperations.PendCooperation;
using Trendlink.Application.Exceptions;
using Trendlink.Application.UnitTests.Advertisements;
using Trendlink.Application.UnitTests.Conditions;
using Trendlink.Application.UnitTests.Users;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Cooperations
{
    public class PendCooperationTests
    {
        private static readonly PendCooperationCommand Command =
            new(
                CooperationData.Name,
                CooperationData.Description,
                CooperationData.ScheduledOnUtc,
                CooperationData.AdvertisementId,
                CooperationData.SellerId
            );

        private readonly IUserRepository _userRepositoryMock;
        private readonly IConditionRepository _conditionRepositoryMock;
        private readonly IAdvertisementRepository _advertisementRepositoryMock;
        private readonly ICooperationRepository _cooperationRepositoryMock;
        private readonly IUserContext _userContextMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly PendCooperationCommandHandler _handler;

        public PendCooperationTests()
        {
            this._userRepositoryMock = Substitute.For<IUserRepository>();
            this._conditionRepositoryMock = Substitute.For<IConditionRepository>();
            this._advertisementRepositoryMock = Substitute.For<IAdvertisementRepository>();
            this._cooperationRepositoryMock = Substitute.For<ICooperationRepository>();
            this._userContextMock = Substitute.For<IUserContext>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.UtcNow.Returns(CooperationData.UtcNow);

            this._handler = new PendCooperationCommandHandler(
                this._userRepositoryMock,
                this._conditionRepositoryMock,
                this._advertisementRepositoryMock,
                this._cooperationRepositoryMock,
                this._userContextMock,
                dateTimeProvider,
                this._unitOfWorkMock
            );
        }

        private void SetupCommonMocks(User user, Condition condition, Advertisement advertisement)
        {
            this._userRepositoryMock.GetByIdAsync(Command.SellerId, default).Returns(user);

            this._userContextMock.UserId.Returns(user.Id);

            this._conditionRepositoryMock.GetByUserIdWithAdvertisementAsync(
                Command.SellerId,
                default
            )
                .Returns(condition);

            this._advertisementRepositoryMock.GetByIdAsync(Command.AdvertisementId, default)
                .Returns(advertisement);

            FieldInfo? advertisementsField = typeof(Condition).GetField(
                "_advertisements",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            if (advertisementsField is not null)
            {
                var advertisements = (List<Advertisement>)advertisementsField.GetValue(condition);
                advertisements!.Add(advertisement);
            }
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenSellerIsNull()
        {
            // Arrange
            this._userRepositoryMock.GetByIdAsync(Command.SellerId, default).Returns((User?)null);

            // Act
            Result<CooperationId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UserErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenBuyerAndSellerAreSameUser()
        {
            // Arrange
            User user = UserData.Create();

            this._userRepositoryMock.GetByIdAsync(Command.SellerId, default).Returns(user);

            this._userContextMock.UserId.Returns(Command.SellerId);

            // Act
            Result<CooperationId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.SameUser);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenSellerHasNoCondition()
        {
            // Arrange
            User user = UserData.Create();

            this._userRepositoryMock.GetByIdAsync(Command.SellerId, default).Returns(user);

            this._userContextMock.UserId.Returns(user.Id);

            this._conditionRepositoryMock.GetByUserIdWithAdvertisementAsync(
                Command.SellerId,
                default
            )
                .Returns((Condition?)null);

            // Act
            Result<CooperationId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ConditionErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenAdvertisementIsNull()
        {
            // Arrange
            User user = UserData.Create();

            this._userRepositoryMock.GetByIdAsync(Command.SellerId, default).Returns(user);

            this._userContextMock.UserId.Returns(user.Id);

            this._conditionRepositoryMock.GetByUserIdWithAdvertisementAsync(
                Command.SellerId,
                default
            )
                .Returns(ConditionData.Create());

            this._advertisementRepositoryMock.GetByIdAsync(Command.AdvertisementId, default)
                .Returns((Advertisement?)null);

            // Act
            Result<CooperationId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AdvertisementErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenAdvertisementNotFound()
        {
            // Arrange
            User user = UserData.Create();

            this._userRepositoryMock.GetByIdAsync(Command.SellerId, default).Returns(user);

            this._userContextMock.UserId.Returns(user.Id);

            this._conditionRepositoryMock.GetByUserIdWithAdvertisementAsync(
                Command.SellerId,
                default
            )
                .Returns(ConditionData.Create());

            this._advertisementRepositoryMock.GetByIdAsync(Command.AdvertisementId, default)
                .Returns(AdvertisementData.Create());

            // Act
            Result<CooperationId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AdvertisementErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenCooperationIsAlreadyStarted()
        {
            // Arrange
            User user = UserData.Create();
            Condition condition = ConditionData.Create();
            Advertisement advertisement = AdvertisementData.Create();

            this.SetupCommonMocks(user, condition, advertisement);

            this._cooperationRepositoryMock.IsAlreadyStarted(
                advertisement,
                Command.ScheduledOnUtc,
                default
            )
                .Returns(true);

            // Act
            Result<CooperationId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.AlreadyStarted);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUnitOfWorkThrows()
        {
            // Arrange
            User user = UserData.Create();

            Condition condition = ConditionData.Create();

            Advertisement advertisement = AdvertisementData.Create();

            this.SetupCommonMocks(user, condition, advertisement);

            this._cooperationRepositoryMock.IsAlreadyStarted(
                advertisement,
                Command.ScheduledOnUtc,
                default
            )
                .Returns(false);

            this._unitOfWorkMock.SaveChangesAsync(default)
                .Throws(new ConcurrencyException("Concurrency", new Exception()));

            // Act
            Result<CooperationId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CooperationErrors.AlreadyStarted);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            User user = UserData.Create();

            Condition condition = ConditionData.Create();

            Advertisement advertisement = AdvertisementData.Create();

            this.SetupCommonMocks(user, condition, advertisement);

            this._cooperationRepositoryMock.IsAlreadyStarted(
                advertisement,
                Command.ScheduledOnUtc,
                default
            )
                .Returns(false);

            // Act
            Result<CooperationId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();

            this._cooperationRepositoryMock.Received(1)
                .Add(Arg.Is<Cooperation>(cooperation => cooperation.Id == result.Value));
        }
    }
}
