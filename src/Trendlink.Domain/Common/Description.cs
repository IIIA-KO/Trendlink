namespace Trendlink.Domain.Common
{
    public sealed record Description(string Value)
    {
        public static explicit operator string(Description description)
        {
            ArgumentNullException.ThrowIfNull(description);
            
            return description.Value;
        }
    }
}
