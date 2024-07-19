using FluentAssertions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.UnitTests.Infrastructure;
using Trendlink.Domain.Users.States;

namespace Trendlink.Domain.UnitTests.States
{
    public class StateTests : BaseTest
    {
        [Fact]
        public void Create_Should_CreateState_WhenValidNameAndCountryProvided()
        {
            // Act
            Result<State> result = State.Create(StateData.StateName, StateData.Country);

            // Assert
            result.IsSuccess.Should().BeTrue();
            State createdCity = result.Value;
            createdCity.Name.Should().Be(StateData.StateName);
            createdCity.Country.Should().Be(StateData.Country);
        }

        [Fact]
        public void Create_Should_Fail_WhenNameIsNull()
        {
            // Act
            Result<State> result = State.Create(null!, StateData.Country);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(StateErrors.Invalid);
        }

        [Fact]
        public void Create_Should_Fail_WhenNameValueIsNull()
        {
            // Arrange
            var stateName = new StateName(null!);

            // Act
            Result<State> result = State.Create(stateName, StateData.Country);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(StateErrors.Invalid);
        }

        [Fact]
        public void Create_Should_Fail_WhenNameValueIsEmpty()
        {
            // Arrange
            var stateName = new StateName(string.Empty);

            // Act
            Result<State> result = State.Create(stateName, StateData.Country);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(StateErrors.Invalid);
        }

        [Fact]
        public void Create_Should_Fail_WhenCountryIsNull()
        {
            // Act
            Result<State> result = State.Create(StateData.StateName, null);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(StateErrors.Invalid);
        }
    }
}
