using FluentAssertions;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;

namespace Trendlink.Domain.UnitTests.Monies
{
    public class MoneyTests
    {
        [Fact]
        public void Addition_Should_ReturnCorrectSum_WhenCurrenciesAreSame()
        {
            // Arrange
            var money1 = new Money(100, Currency.Usd);
            var money2 = new Money(50, Currency.Usd);

            // Act
            Money result = money1 + money2;

            // Assert
            result.Amount.Should().Be(150);
            result.Currency.Should().Be(Currency.Usd);
        }

        [Fact]
        public void Addition_Should_ThrowInvalidOperationException_WhenCurrenciesAreDifferent()
        {
            // Arrange
            var money1 = new Money(100, Currency.Usd);
            var money2 = new Money(50, Currency.Eur);

            // Act
            Action act = () =>
            {
                Money result = money1 + money2;
                Console.WriteLine(result);
            };

            // Assert
            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Currencies have to be equal.");
        }

        [Fact]
        public void Subtraction_Should_ReturnCorrectDifference_WhenCurrenciesAreSame()
        {
            // Arrange
            var money1 = new Money(100, Currency.Usd);
            var money2 = new Money(50, Currency.Usd);

            // Act
            Money result = money1 - money2;

            // Assert
            result.Amount.Should().Be(50);
            result.Currency.Should().Be(Currency.Usd);
        }

        [Fact]
        public void Subtraction_Should_ThrowInvalidOperationException_WhenCurrenciesAreDifferent()
        {
            // Arrange
            var money1 = new Money(100, Currency.Usd);
            var money2 = new Money(50, Currency.Eur);

            // Act
            Action subtraction = () =>
            {
                Money result = money1 - money2;
                Console.WriteLine(result);
            };

            // Assert
            subtraction
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Currencies have to be equal.");
        }

        [Fact]
        public void Subtraction_Should_ThrowInvalidOperationException_WhenResultIsNegative()
        {
            // Arrange
            var money1 = new Money(50, Currency.Usd);
            var money2 = new Money(100, Currency.Usd);

            // Act
            Action subtraction = () =>
            {
                Money result = money1 - money2;
                Console.WriteLine(result);
            };

            // Assert
            subtraction
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Not enough funds.");
        }

        [Fact]
        public void Multiplication_Should_ReturnCorrectProduct()
        {
            // Arrange
            var money = new Money(100, Currency.Usd);

            // Act
            Money result = money * 2;

            // Assert
            result.Amount.Should().Be(200);
            result.Currency.Should().Be(Currency.Usd);
        }

        [Fact]
        public void Division_Should_ReturnCorrectQuotient()
        {
            // Arrange
            var money = new Money(100, Currency.Usd);

            // Act
            Money result = money / 2;

            // Assert
            result.Amount.Should().Be(50);
            result.Currency.Should().Be(Currency.Usd);
        }

        [Fact]
        public void Division_Should_ThrowInvalidOperationException_WhenDividingByZero()
        {
            // Arrange
            var money = new Money(100, Currency.Usd);

            // Act
            Action division = () =>
            {
                Money result = money / 0;
                Console.WriteLine(result);
            };

            // Assert
            division.Should().Throw<DivideByZeroException>();
        }

        [Fact]
        public void GreaterThan_Should_ReturnTrue_WhenLeftIsGreaterThanRight()
        {
            // Arrange
            var money1 = new Money(100, Currency.Usd);
            var money2 = new Money(50, Currency.Usd);

            // Act
            bool result = money1 > money2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void GreaterThan_Should_ThrowArgumentException_WhenCurrenciesAreDifferent()
        {
            // Arrange
            var money1 = new Money(100, Currency.Usd);
            var money2 = new Money(50, Currency.Eur);

            // Act
            Action greaterThan = () =>
            {
                bool result = money1 > money2;
                Console.WriteLine(result);
            };

            // Assert
            greaterThan
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Cannot compare USD and EUR");
        }

        [Fact]
        public void LessThan_Should_ReturnTrue_WhenLeftIsLessThanRight()
        {
            // Arrange
            var money1 = new Money(50, Currency.Usd);
            var money2 = new Money(100, Currency.Usd);

            // Act
            bool result = money1 < money2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void LessThan_Should_ThrowArgumentException_WhenCurrenciesAreDifferent()
        {
            // Arrange
            var money1 = new Money(50, Currency.Usd);
            var money2 = new Money(100, Currency.Eur);

            // Act
            Action lessThan = () =>
            {
                bool result = money1 < money2;
                Console.WriteLine(result);
            };

            // Assert
            lessThan.Should().Throw<ArgumentException>().WithMessage("Cannot compare USD and EUR");
        }

        [Fact]
        public void GreaterThanOrEqual_Should_ReturnTrue_WhenLeftIsGreaterThanOrEqualRight()
        {
            // Arrange
            var money1 = new Money(100, Currency.Usd);
            var money2 = new Money(100, Currency.Usd);

            // Act
            bool result = money1 >= money2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void GreaterThanOrEqual_Should_ThrowArgumentException_WhenCurrenciesAreDifferent()
        {
            // Arrange
            var money1 = new Money(100, Currency.Usd);
            var money2 = new Money(100, Currency.Eur);

            // Act
            Action greaterThanOrEqual = () =>
            {
                bool result = money1 >= money2;
                Console.WriteLine(result);
            };

            // Assert
            greaterThanOrEqual
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Cannot compare USD and EUR");
        }

        [Fact]
        public void LessThanOrEqual_Should_ReturnTrue_WhenLeftIsLessThanOrEqualRight()
        {
            // Arrange
            var money1 = new Money(50, Currency.Usd);
            var money2 = new Money(100, Currency.Usd);

            // Act
            bool result = money1 <= money2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void LessThanOrEqual_Should_ThrowArgumentException_WhenCurrenciesAreDifferent()
        {
            // Arrange
            var money1 = new Money(50, Currency.Usd);
            var money2 = new Money(100, Currency.Eur);

            // Act
            Action lessThanOrEqual = () =>
            {
                bool result = money1 <= money2;
                Console.WriteLine(result);
            };

            // Assert
            lessThanOrEqual
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Cannot compare USD and EUR");
        }

        [Fact]
        public void IsZero_Should_ReturnTrue_WhenAmountIsZero()
        {
            // Arrange
            var money = new Money(0, Currency.Usd);

            // Act
            bool result = money.IsZero();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsZero_Should_ReturnFalse_WhenAmountIsNotZero()
        {
            // Arrange
            var money = new Money(100, Currency.Usd);

            // Act
            bool result = money.IsZero();

            // Assert
            result.Should().BeFalse();
        }
    }
}
