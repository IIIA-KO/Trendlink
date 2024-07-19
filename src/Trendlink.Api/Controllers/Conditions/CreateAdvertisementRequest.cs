using System.Text.Json.Serialization;

namespace Trendlink.Api.Controllers.Conditions
{
    public sealed class CreateAdvertisementRequest
    {
        public string Name { get; init; }

        [JsonRequired]
        public decimal PriceAmount { get; init; }

        public string PriceCurrency { get; init; }

        public string Description { get; init; }
    }
}
