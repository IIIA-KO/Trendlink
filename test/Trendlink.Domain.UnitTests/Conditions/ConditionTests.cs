using FluentAssertions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.ValueObjects;

namespace Trendlink.Domain.UnitTests.Conditions
{
    public class ConditionTests
    {
        [Fact]
        public void Create_Should_SetPropertyValues()
        {
            // Act
            Condition condition = Condition
                .Create(ConditionData.UserId, ConditionData.Description)
                .Value;

            // Assert
            condition.UserId.Should().Be(ConditionData.UserId);
            condition.Description.Should().Be(ConditionData.Description);
            condition.Advertisements.Should().BeEmpty();
            condition.User.Should().BeNull();
        }

        [Fact]
        public void Create_Should_ReturnFailure_WhenDescriptionIsNull()
        {
            // Act
            Result<Condition> result = Condition.Create(
                ConditionData.UserId,
                new Description(null!)
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ConditionErrors.InvalidDescription);
        }
    }
}
