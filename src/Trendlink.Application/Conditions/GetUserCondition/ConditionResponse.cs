namespace Trendlink.Application.Conditions.GetUserCondition
{
    public sealed class ConditionResponse
    {
        public ConditionResponse() { }

        public Guid Id { get; init; }

        public Guid UserId { get; init; }

        public string Description { get; init; }

        public List<AdvertisementResponse> Advertisements { get; set; } = [];
    }

    public sealed class AdvertisementResponse
    {
        public AdvertisementResponse() { }

        public Guid Id { get; init; }

        public string Name { get; init; }

        public decimal PriceAmount { get; init; }

        public string PriceCurrency { get; init; }

        public string Description { get; init; }
    }
}
