namespace Trendlink.Domain.Shared
{
    public sealed record Description(string Value)
    {
        public static explicit operator string(Description description) => description.Value;
    }
}
