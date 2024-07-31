using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Conditions.CreateCondition;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;

namespace Trendlink.Application.UnitTests.Conditions
{
    public class CreateConditionTests
    {
        public static readonly CreateConditionCommand Command = new(ConditionData.Description);

        private readonly IUserContext _userContextMock;
        private readonly IConditionRepository _conditionRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly CreateConditionCommandHandler _handler;

        public CreateConditionTests()
        {
            this._userContextMock = Substitute.For<IUserContext>();
            this._conditionRepositoryMock = Substitute.For<IConditionRepository>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new CreateConditionCommandHandler(
                this._userContextMock,
                this._conditionRepositoryMock,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Hadle_Should_ReturnFailure_WhenConditionExist()
        {
            // Arrange
            this._userContextMock.UserId.Returns(ConditionData.UserId);

            this._conditionRepositoryMock.ExistsByUserId(ConditionData.UserId, default)
                .Returns(true);

            // Act
            Result<ConditionId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ConditionErrors.Duplicate);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenConditionCreationFails()
        {
            // Arrange
            this._userContextMock.UserId.Returns(ConditionData.UserId);

            this._conditionRepositoryMock.ExistsByUserId(ConditionData.UserId, default)
                .Returns(false);

            var invalidCommand = new CreateConditionCommand(null!);

            // Act
            Result<ConditionId> result = await this._handler.Handle(invalidCommand, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ConditionErrors.InvalidDescription);
        }

        [Fact]
        public async Task Handle_Should_SuccessfullyCreateCondition()
        {
            // Arrange
            this._userContextMock.UserId.Returns(ConditionData.UserId);

            this._conditionRepositoryMock.ExistsByUserId(ConditionData.UserId, default)
                .Returns(false);

            // Act
            Result<ConditionId> result = await this._handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
