namespace Trendlink.Domain.Common
{
    public sealed record Name(string Value)
    {
        public static explicit operator string(Name name)
        {
            ArgumentNullException.ThrowIfNull(name);
            
            return name.Value;
        }
    }
}
