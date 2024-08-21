using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Conditions.EditLoggedInUserCondition;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.UnitTests.Conditions
{
    public class EditLoggedInUserCondtionTests
    {
        public static readonly EditLoggedInUserConditionCommand Command =
            new(ConditionData.Description);

        public static readonly UserId LoggedInUserId = ConditionData.UserId;

        private readonly IUserContext _userContextMock;
        private readonly IConditionRepository _conditionRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private readonly EditLoggedInUserConditionCommandHandler _handler;

        public EditLoggedInUserCondtionTests()
        {
            this._userContextMock = Substitute.For<IUserContext>();
            this._conditionRepositoryMock = Substitute.For<IConditionRepository>();
            this._unitOfWorkMock = Substitute.For<IUnitOfWork>();

            this._handler = new EditLoggedInUserConditionCommandHandler(
                this._userContextMock,
                this._conditionRepositoryMock,
                this._unitOfWorkMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenConditionIsNull()
        {
            // Arrange
            this._userContextMock.UserId.Returns(LoggedInUserId);

            this._conditionRepositoryMock.GetByUserIdAsync(ConditionData.UserId, default)
                .Returns((Condition?)null);

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Act
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ConditionErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenUpdateFails()
        {
            // Arrange
            this._userContextMock.UserId.Returns(LoggedInUserId);

            this._conditionRepositoryMock.GetByUserIdAsync(ConditionData.UserId, default)
                .Returns(ConditionData.Create());

            var invalidCommand = new EditLoggedInUserConditionCommand(null!);

            // Act
            Result result = await this._handler.Handle(invalidCommand, default);

            // Act
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ConditionErrors.InvalidDescription);
        }

        [Fact]
        public async Task Handle_Should_SuccessfullyEditCondition()
        {
            // Arrange
            this._userContextMock.UserId.Returns(LoggedInUserId);

            this._conditionRepositoryMock.GetByUserIdAsync(ConditionData.UserId, default)
                .Returns(ConditionData.Create());

            // Act
            Result result = await this._handler.Handle(Command, default);

            // Act
            result.IsSuccess.Should().BeTrue();
        }
    }
}
