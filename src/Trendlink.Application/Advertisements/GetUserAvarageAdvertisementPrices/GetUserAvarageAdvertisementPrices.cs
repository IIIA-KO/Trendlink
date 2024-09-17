using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Advertisements.GetUserAvarageAdvertisementPrices
{
    public sealed record GetUserAvarageAdvertisementPrices(UserId UserId)
        : IQuery<List<AvaragePriceResponse>>;
}
