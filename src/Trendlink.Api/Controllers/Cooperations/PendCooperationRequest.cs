using System.Text.Json.Serialization;

namespace Trendlink.Api.Controllers.Cooperations
{
    public sealed class PendCooperationRequest
    {
        public string Name { get; init; }

        public string Description { get; init; }

        [JsonRequired]
        public DateTimeOffset ScheduledOnUtc { get; init; }

        [JsonRequired]
        public decimal PriceAmount { get; init; }

        public string PriceCurrency { get; init; }

        [JsonRequired]
        public Guid AdvertisementId { get; init; }
    }
}
