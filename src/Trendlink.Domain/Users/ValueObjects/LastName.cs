namespace Trendlink.Domain.Users.ValueObjects
{
    public sealed record LastName(string Value)
    {
        public static explicit operator string(LastName lastName) => lastName.Value;
    }
}
