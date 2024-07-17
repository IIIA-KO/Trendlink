using FluentAssertions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;
using Trendlink.Domain.Conditions.ValueObjects;

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
    }
}
