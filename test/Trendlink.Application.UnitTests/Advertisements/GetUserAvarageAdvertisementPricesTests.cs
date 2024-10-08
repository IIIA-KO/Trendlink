using System.Reflection;
using FluentAssertions;
using NSubstitute;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Advertisements.GetUserAvarageAdvertisementPrices;
using Trendlink.Application.UnitTests.Conditions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Advertisements
{
    public class GetUserAvarageAdvertisementPricesTests
    {
        private static readonly GetUserAvarageAdvertisementPricesQuery Query = new(UserId.New());

        private readonly GetUserAvarageAdvertisementPricesQueryHandler _handler;

        private readonly IConditionRepository _conditionRepositoryMock;

        public GetUserAvarageAdvertisementPricesTests()
        {
            this._conditionRepositoryMock = Substitute.For<IConditionRepository>();

            this._handler = new GetUserAvarageAdvertisementPricesQueryHandler(
                this._conditionRepositoryMock
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenConditionIsNull()
        {
            // Arrange
            this._conditionRepositoryMock.GetByUserIdWithAdvertisementAsync(
                Arg.Any<UserId>(),
                default
            )
                .Returns((Condition?)null);

            // Act
            Result<List<AvaragePriceResponse>> result = await this._handler.Handle(Query, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ConditionErrors.NotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnEmptyList_WhenConditionHasNoAdvertisements()
        {
            // Arrange
            this._conditionRepositoryMock.GetByUserIdWithAdvertisementAsync(
                Arg.Any<UserId>(),
                default
            )
                .Returns(ConditionData.Create());

            // Act
            Result<List<AvaragePriceResponse>> result = await this._handler.Handle(Query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_Should_ReturnAveragePrices_WhenAdvertisementsExist()
        {
            var advertisements = new List<Advertisement>
            {
                AdvertisementData.Create(100, "USD"),
                AdvertisementData.Create(200, "USD"),
                AdvertisementData.Create(300, "EUR"),
            };

            Condition condition = ConditionData.Create();

            FieldInfo? advertisementsField = typeof(Condition).GetField(
                "_advertisements",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            if (advertisementsField is not null)
            {
                var advertisementsList =
                    (List<Advertisement>)advertisementsField.GetValue(condition);
                advertisements.ForEach(a => advertisementsList!.Add(a));
            }

            this._conditionRepositoryMock.GetByUserIdWithAdvertisementAsync(
                Arg.Any<UserId>(),
                default
            )
                .Returns(condition);

            // Act
            Result<List<AvaragePriceResponse>> result = await this._handler.Handle(Query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(2);

            decimal usdAverage = result.Value.First(r => r.Currency == "USD").Value;
            decimal eurAverage = result.Value.First(r => r.Currency == "EUR").Value;

            usdAverage.Should().Be(150);
            eurAverage.Should().Be(300);
        }
    }
}
