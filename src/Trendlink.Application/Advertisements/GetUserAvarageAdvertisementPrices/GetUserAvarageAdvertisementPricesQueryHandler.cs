using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Advertisements.GetUserAvarageAdvertisementPrices
{
    internal sealed class GetUserAvarageAdvertisementPricesQueryHandler
        : IQueryHandler<GetUserAvarageAdvertisementPrices, List<AvaragePriceResponse>>
    {
        private readonly IConditionRepository _conditionRepository;

        public GetUserAvarageAdvertisementPricesQueryHandler(
            IConditionRepository conditionRepository
        )
        {
            this._conditionRepository = conditionRepository;
        }

        public async Task<Result<List<AvaragePriceResponse>>> Handle(
            GetUserAvarageAdvertisementPrices request,
            CancellationToken cancellationToken
        )
        {
            Condition? condition =
                await this._conditionRepository.GetByUserIdWithAdvertisementAsync(
                    request.UserId,
                    cancellationToken
                );
            if (condition is null)
            {
                return Result.Failure<List<AvaragePriceResponse>>(ConditionErrors.NotFound);
            }

            var avaragePriceResponses = new List<AvaragePriceResponse>();
            if (condition.Advertisements?.Any() == true)
            {
                foreach (
                    IGrouping<
                        string,
                        Advertisement
                    > currencyGroup in condition.Advertisements.GroupBy(a => a.Price.Currency.Code)
                )
                {
                    decimal averagePrice = currencyGroup.Average(a => a.Price);
                    avaragePriceResponses.Add(
                        new AvaragePriceResponse(currencyGroup.Key, averagePrice)
                    );
                }
            }

            return avaragePriceResponses;
        }
    }
}
