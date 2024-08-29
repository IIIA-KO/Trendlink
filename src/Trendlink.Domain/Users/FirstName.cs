namespace Trendlink.Domain.Users
{
    public sealed record FirstName(string Value)
    {
        public static explicit operator string(FirstName firstName) => firstName.Value;
    }
}
