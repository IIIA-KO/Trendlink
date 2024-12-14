namespace Trendlink.Domain.Conditions.Advertisements
{
    public sealed class Currency
    {
        private Currency(string code)
        {
            this.Code = code;
        }

        public string Code { get; init; }

        internal static readonly Currency None = new(string.Empty);

        public static readonly Currency Usd = new("USD");
        public static readonly Currency Eur = new("EUR");
        public static readonly Currency Uah = new("UAH");

        private static readonly IReadOnlyCollection<Currency> _all = [Usd, Eur, Uah];

        public static Currency FromCode(string code)
        {
            return _all.FirstOrDefault(currency =>
                    currency.Code.Equals(code, StringComparison.OrdinalIgnoreCase)
                ) ?? throw new InvalidCastException("The currency is invalid.");
        }

        public Money MinPositiveValue => new(.01M, this);

        public Money Of(decimal amount) => new(amount, this);

        public override string ToString() => this.Code;
    }
}
