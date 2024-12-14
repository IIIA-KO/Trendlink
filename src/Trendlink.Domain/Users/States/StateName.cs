namespace Trendlink.Domain.Users.States
{
    public sealed record StateName(string Value)
    {
        public static explicit operator string(StateName stateName)
        {
            ArgumentNullException.ThrowIfNull(stateName);
            return stateName.Value;
        }
    }
}
