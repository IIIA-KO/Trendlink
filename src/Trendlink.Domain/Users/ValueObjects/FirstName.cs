namespace Trendlink.Domain.Users.ValueObjects
{
    public sealed record FirstName(string Value)
    {
        public static explicit operator string(FirstName firstName) => firstName.Value;
    }
}
