using FluentAssertions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Common;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.UnitTests.Advertisements;

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

        [Fact]
        public void Update_Should_UpdateConditionPropertes()
        {
            // Arrange
            Condition condition = Condition
                .Create(ConditionData.UserId, ConditionData.Description)
                .Value;

            var newDescription = new Description("New Description");

            // Act
            condition.Update(newDescription);

            // Assert
            condition.Description.Should().Be(newDescription);
        }

        [Fact]
        public void Update_Should_ReturnFailure_WhenDescriptionIsNull()
        {
            // Arrange
            Condition condition = Condition
                .Create(ConditionData.UserId, ConditionData.Description)
                .Value;

            // Act
            Result result = condition.Update(null!);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ConditionErrors.InvalidDescription);
        }

        [Fact]
        public void HasAdvertisement_Should_ReturnFalse_WhenAdvertisementNotPresentInList()
        {
            // Arrange
            Condition condition = Condition
                .Create(ConditionData.UserId, ConditionData.Description)
                .Value;

            // Act
            bool result = condition.HasAdvertisement(AdvertisementData.Name);

            // Assert
            result.Should().BeFalse();
        }
    }
}
