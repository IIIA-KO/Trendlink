namespace Trendlink.Infrastructure.BackgroundJobs.InstagramAccounts
{
    internal sealed record InstagramAccountResponse(
        Guid Id,
        Guid UserId,
        string FacebookPageId,
        string MetadataId,
        long MetadataIgId,
        string MetadataUserName,
        int MetadataFollowersCount,
        int MetadataMediaCount
    )
    {
        public InstagramAccountResponse()
            : this(Guid.Empty, Guid.Empty, string.Empty, string.Empty, 0, string.Empty, 0, 0) { }
    }
}
