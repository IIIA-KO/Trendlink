namespace Trendlink.Domain.Users.States
{
    public record StateId(Guid Value)
    {
        public static StateId New() => new(Guid.NewGuid());
    }
}
