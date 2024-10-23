using FluentAssertions;
using Trendlink.Domain.Conditions.Advertisements;

namespace Trendlink.Domain.UnitTests.Currencies
{
    public class CurrencyTests
    {
        [Fact]
        public void FromCode_Should_ReturnCorrectCurrency_WhenCodeIsValid()
        {
            // Act
            var currency = Currency.FromCode("USD");

            // Assert
            currency.Should().Be(Currency.Usd);
        }

        [Fact]
        public void FromCode_Should_ThrowApplicationException_WhenCodeIsInvalid()
        {
            // Act
            Action fromCode = () =>
            {
                var currencyFromCode = Currency.FromCode("INVALID");
                Console.WriteLine(currencyFromCode);
            };

            // Assert
            fromCode
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("The currency is invalid.");
        }

        [Fact]
        public void ToString_Should_ReturnCurrencyCode()
        {
            // Arrange
            Currency currency = Currency.Usd;

            // Act
            string code = currency.ToString();

            // Assert
            code.Should().Be("USD");
        }
    }
}
