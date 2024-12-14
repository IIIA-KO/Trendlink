namespace Trendlink.Domain.Conditions.Advertisements
{
    public sealed record Money(decimal Amount, Currency Currency)
    {
        public static Money operator +(Money left, Money right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            
            if (left.Currency != right.Currency)
            {
                throw new InvalidOperationException("Currencies have to be equal.");
            }

            return new Money(left.Amount + right.Amount, left.Currency);
        }
        
        public static Money Add(Money left, Money right) => left + right;

        public static Money operator -(Money left, Money right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            
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
        
        public static Money Subtract(Money left, Money right) => left - right;

        public static Money operator *(Money amount, decimal factor)
        {
            ArgumentNullException.ThrowIfNull(amount);
            return new Money(amount.Amount * factor, amount.Currency);
        }
        
        public static Money Multiply(Money left, Money right) => left * right;
        
        public static Money operator /(Money amount, decimal factor)
        {
            ArgumentNullException.ThrowIfNull(amount);
            return new Money(amount.Amount / factor, amount.Currency);
        }
        
        public static decimal operator /(Money left, Money right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            
            return left.Currency == right.Currency
                ? left.Amount / right.Amount
                : RaiseCurrencyError<decimal>("divide", left, right);
        }
        
        public static decimal Divide(Money left, Money right) => left / right;

        public static bool operator >(Money left, Money right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            
            return left.Currency == right.Currency
                ? left.Amount > right.Amount
                : RaiseCurrencyComparisonError(left, right);
        }

        public static bool operator <(Money left, Money right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            
            return left.Currency == right.Currency
                ? left.Amount < right.Amount
                : RaiseCurrencyComparisonError(left, right);
        }
        
        public static bool operator >=(Money left, Money right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            
            return left.Currency == right.Currency
                ? left.Amount >= right.Amount
                : RaiseCurrencyComparisonError(left, right);
        }

        public static bool operator <=(Money left, Money right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            
            return left.Currency == right.Currency
                ? left.Amount <= right.Amount
                : RaiseCurrencyComparisonError(left, right);
        }

        public int CompareTo(Money other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            if (other.Currency != this.Currency)
            {
                return 1;
            }
            
            return this.Amount.CompareTo(other.Amount);
        }
        
        public static implicit operator decimal(Money a)
        {
            ArgumentNullException.ThrowIfNull(a);
            return a.Amount;
        }

        public static decimal ToDecimal(Money a)
        {
            return (decimal)a;
        }
        
        private static bool RaiseCurrencyComparisonError(Money a, Money b) =>
            RaiseCurrencyError<bool>("compare", a, b);

        private static T RaiseCurrencyError<T>(string operation, Money a, Money b) =>
            throw new ArgumentException($"Cannot {operation} {a.Currency} and {b.Currency}");

        public static Money Zero() => new(0, Currency.None);

        public static Money Zero(Currency currency) => new(0, currency);

        public bool IsZero() => this == Zero(this.Currency);

        public override string ToString()
        {
            return $"{this.Amount} {this.Currency}";
        }
    }
}
