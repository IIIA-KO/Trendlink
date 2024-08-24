namespace Trendlink.Domain.Users.InstagramBusinessAccount
{
    public sealed record InstagramAccountId(Guid Value)
    {
        public static InstagramAccountId New() => new(Guid.NewGuid());
    }
}
