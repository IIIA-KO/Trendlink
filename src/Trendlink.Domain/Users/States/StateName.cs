using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Domain.Users.States
{
    public sealed record StateName(string Value)
    {
        public static explicit operator string(StateName firstName) => firstName.Value;
    }
}
