using FluentAssertions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Shared;

namespace Trendlink.Domain.UnitTests.Advertisements
{
    public class AdvertisementTests
    {
        [Fact]
        public void Create_Should_SetPropertyValues()
        {
            // Act
            Advertisement advertisement = Advertisement
                .Create(
                    AdvertisementData.ConditionId,
                    AdvertisementData.Name,
                    AdvertisementData.Price,
                    AdvertisementData.Description
                )
                .Value;

            // Assert
            advertisement.ConditionId.Should().Be(AdvertisementData.ConditionId);
            advertisement.Name.Should().Be(AdvertisementData.Name);
            advertisement.Price.Should().Be(AdvertisementData.Price);
            advertisement.Description.Should().Be(AdvertisementData.Description);
            advertisement.Condition.Should().BeNull();
        }

        [Fact]
        public void Create_Should_ReturnFailure_When_NameIsNull()
        {
            // Act
            Result<Advertisement> result = Advertisement.Create(
                AdvertisementData.ConditionId,
                new Name(null!),
                AdvertisementData.Price,
                AdvertisementData.Description
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AdvertisementErrors.Invalid);
        }

        [Fact]
        public void Create_Should_ReturnFailure_When_DescriptionIsInvalid()
        {
            // Act
            Result<Advertisement> result = Advertisement.Create(
                AdvertisementData.ConditionId,
                AdvertisementData.Name,
                AdvertisementData.Price,
                new Description(string.Empty)
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AdvertisementErrors.Invalid);
        }

        [Fact]
        public void Create_Should_ReturnFailure_When_PriceIsInvalid()
        {
            // Act
            Result<Advertisement> result = Advertisement.Create(
                AdvertisementData.ConditionId,
                AdvertisementData.Name,
                Money.Zero(),
                AdvertisementData.Description
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AdvertisementErrors.InvalidPrice);
        }

        [Fact]
        public void Update_Should_UpdateAdvertisementProperties()
        {
            // Arrange
            Advertisement advertisement = Advertisement
                .Create(
                    AdvertisementData.ConditionId,
                    AdvertisementData.Name,
                    AdvertisementData.Price,
                    AdvertisementData.Description
                )
                .Value;

            var newName = new Name("New Name");
            var newPrice = new Money(100, Currency.Uah);
            var newDescription = new Description("New Description");

            // Act
            advertisement.Update(newName, newPrice, newDescription);

            // Assert
            advertisement.Name.Should().Be(newName);
            advertisement.Price.Should().Be(newPrice);
            advertisement.Description.Should().Be(newDescription);
        }

        [Fact]
        public void Update_Should_ReturnFailure_WhenPriceAmountIsInvalid()
        {
            // Arrange
            Advertisement advertisement = Advertisement
                .Create(
                    AdvertisementData.ConditionId,
                    AdvertisementData.Name,
                    AdvertisementData.Price,
                    AdvertisementData.Description
                )
                .Value;

            var newName = new Name("New Name");
            var newPrice = new Money(-23.2m, Currency.Uah);
            var newDescription = new Description("New Description");

            // Act
            Result result = advertisement.Update(newName, newPrice, newDescription);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AdvertisementErrors.InvalidPrice);
        }
    }
}
