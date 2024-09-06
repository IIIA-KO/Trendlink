namespace Trendlink.Domain.Users.InstagramBusinessAccount
{
    public sealed record Metadata(
        string Id,
        long IgId,
        string UserName,
        int FollowersCount,
        int MediaCount
    );
}
