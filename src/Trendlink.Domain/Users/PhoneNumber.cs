namespace Trendlink.Domain.Users
{
    public sealed record PhoneNumber(string Value)
    {
        public static explicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
    }
}
