using FluentAssertions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.UnitTests.Infrastructure;
using Trendlink.Domain.Users.Countries;

namespace Trendlink.Domain.UnitTests.Countries
{
    public class CountryTests : BaseTest
    {
        private static readonly CountryName CountryName = new("TestCountry");

        [Fact]
        public void Create_Should_CreateCountry_WhenValidNameProvided()
        {
            // Arrange
            CountryName countryName = CountryName;

            // Act
            Result<Country> result = Country.Create(countryName);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Name.Should().Be(countryName);
        }

        [Fact]
        public void Create_Should_Fail_WhenNameIsNull()
        {
            // Act
            Result<Country> result = Country.Create(null!);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CountryErrors.Invalid);
        }

        [Fact]
        public void Create_Should_Fail_WhenNameValueIsNull()
        {
            // Act
            Result<Country> result = Country.Create(new CountryName(null!));

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CountryErrors.Invalid);
        }

        [Fact]
        public void Create_Should_Fail_WhenNameValueIsEmpty()
        {
            // Act
            Result<Country> result = Country.Create(new CountryName(string.Empty));

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(CountryErrors.Invalid);
        }
    }
}
