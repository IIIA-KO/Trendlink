using FluentAssertions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.UnitTests.Infrastructure;
using Trendlink.Domain.Users.Cities;

namespace Trendlink.Domain.UnitTests.Cities
{
    public class CityTests : BaseTest
    {
        [Fact]
        public void Create_Should_CreateCity_WhenValidNameAndCountryProvided()
        {
            // Act
            Result<City> result = City.Create(CityData.CityName, CityData.Country);

            // Assert
            result.IsSuccess.Should().BeTrue();
            City createdCity = result.Value;
            createdCity.Name.Should().Be(CityData.CityName);
            createdCity.Country.Should().Be(CityData.Country);
        }

        [Fact]
        public void Create_Should_Fail_WhenNameIsNull()
        {
            // Act
            Result<City> result = City.Create(null!, CityData.Country);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CityErrors.Invalid);
        }

        [Fact]
        public void Create_Should_Fail_WhenNameValueIsNull()
        {
            // Arrange
            var cityName = new CityName(null!);

            // Act
            Result<City> result = City.Create(cityName, CityData.Country);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CityErrors.Invalid);
        }

        [Fact]
        public void Create_Should_Fail_WhenNameValueIsEmpty()
        {
            // Arrange
            var cityName = new CityName(string.Empty);

            // Act
            Result<City> result = City.Create(cityName, CityData.Country);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CityErrors.Invalid);
        }

        [Fact]
        public void Create_Should_Fail_WhenCountryIsNull()
        {
            // Act
            Result<City> result = City.Create(CityData.CityName, null);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CityErrors.Invalid);
        }
    }
}
