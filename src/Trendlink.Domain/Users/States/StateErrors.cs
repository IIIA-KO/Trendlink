using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users.States
{
    public static class StateErrors
    {
        public static readonly Error Invalid = new("State.Invalid", "The state is invalid.");

        public static readonly NotFoundError NotFound =
            new("State.NotFound", "The state with provided identifier was not found");
    }
}
