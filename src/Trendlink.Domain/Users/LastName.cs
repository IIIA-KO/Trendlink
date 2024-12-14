namespace Trendlink.Domain.Users
{
    public sealed record LastName(string Value)
    {
        public static explicit operator string(LastName lastName)
        {
            ArgumentNullException.ThrowIfNull(lastName);
            return lastName.Value;
        }
    }
}
