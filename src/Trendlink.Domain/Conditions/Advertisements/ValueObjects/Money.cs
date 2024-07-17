namespace Trendlink.Domain.Conditions.Advertisements.ValueObjects
{
    public sealed record Money(decimal Amount, Currency Currency)
    {
        public static Money operator +(Money left, Money right)
        {
            if (left.Currency != right.Currency)
            {
                throw new InvalidOperationException("Currencies have to be equal.");
            }

            return new Money(left.Amount + right.Amount, left.Currency);
        }

        public static Money operator -(Money left, Money right)
        {
            if (left.Currency != right.Currency)
            {
                throw new InvalidOperationException("Currencies have to be equal.");
            }

            if (left < right)
            {
                throw new InvalidOperationException("Not enough funds.");
            }

            return new Money(left.Amount - right.Amount, left.Currency);
        }

        public static Money operator *(Money amount, decimal factor) =>
            new(amount.Amount * factor, amount.Currency);

        public static Money operator *(decimal factor, Money amount) =>
            new(amount.Amount * factor, amount.Currency);

        public static Money operator /(Money amount, decimal factor) =>
            new(amount.Amount / factor, amount.Currency);

        public static decimal operator /(Money a, Money b) =>
            a.Currency == b.Currency
                ? a.Amount / b.Amount
                : RaiseCurrencyError<decimal>("divide", a, b);

        public static bool operator >(Money left, Money right) =>
            left.Currency == right.Currency
                ? left.Amount > right.Amount
                : RaiseCurrencyComparisonError(left, right);

        public static bool operator <(Money left, Money right) =>
            left.Currency == right.Currency
                ? left.Amount < right.Amount
                : RaiseCurrencyComparisonError(left, right);

        public static bool operator >=(Money left, Money right) =>
            left.Currency == right.Currency
                ? left.Amount >= right.Amount
                : RaiseCurrencyComparisonError(left, right);

        public static bool operator <=(Money left, Money right) =>
            left.Currency == right.Currency
                ? left.Amount <= right.Amount
                : RaiseCurrencyComparisonError(left, right);

        public static implicit operator decimal(Money a) => a.Amount;

        private static bool RaiseCurrencyComparisonError(Money a, Money b) =>
            RaiseCurrencyError<bool>("compare", a, b);

        private static T RaiseCurrencyError<T>(string operation, Money a, Money b) =>
            throw new ArgumentException($"Cannot {operation} {a.Currency} and {b.Currency}");

        public static Money Zero() => new(0, Currency.None);

        public static Money Zero(Currency currency) => new(0, currency);

        public bool IsZero() => this == Zero(this.Currency);
    }
}
