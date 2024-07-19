namespace Trendlink.Domain.Users.ValueObjects
{
    public record UserId(Guid Value)
    {
        public static UserId New() => new(Guid.NewGuid());
    }
}
