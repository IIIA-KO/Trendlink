namespace Trendlink.Domain.Users.ValueObjects
{
    public sealed record PhoneNumber(string Value)
    {
        public static explicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
    }
}
