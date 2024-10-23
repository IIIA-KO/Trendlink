using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users.InstagramBusinessAccount
{
    public sealed class InstagramAccount : Entity<InstagramAccountId>
    {
        public const int MinFollowersCount = 100;

        private InstagramAccount() { }

        private InstagramAccount(
            InstagramAccountId id,
            UserId userId,
            FacebookPageId facebookPageId,
            AdvertisementAccountId advertisementAccountId,
            Metadata metadata
        )
            : base(id)
        {
            this.UserId = userId;
            this.FacebookPageId = facebookPageId;
            this.AdvertisementAccountId = advertisementAccountId;
            this.Metadata = metadata;
        }

        public UserId UserId { get; private set; }

        public User User { get; init; }

        public FacebookPageId FacebookPageId { get; private set; }

        public AdvertisementAccountId AdvertisementAccountId { get; private set; }

        public Metadata Metadata { get; private set; }

        public DateTime? LastUpdatedAtUtc { get; set; }

        public static Result<InstagramAccount> Create(
            UserId userId,
            FacebookPageId facebookPageId,
            AdvertisementAccountId advertisementAccountId,
            Metadata metadata
        )
        {
            if (string.IsNullOrEmpty(facebookPageId.Value))
            {
                return Result.Failure<InstagramAccount>(
                    InstagramAccountErrors.InvalidFacebookPageId
                );
            }

            if (string.IsNullOrEmpty(advertisementAccountId.Value))
            {
                return Result.Failure<InstagramAccount>(
                    InstagramAccountErrors.InvalidAdvertisementAccountId
                );
            }

            if (string.IsNullOrEmpty(metadata.Id))
            {
                return Result.Failure<InstagramAccount>(InstagramAccountErrors.InvalidId);
            }

            if (metadata.FollowersCount < MinFollowersCount)
            {
                return Result.Failure<InstagramAccount>(InstagramAccountErrors.NotEnoughFollowers);
            }

            return new InstagramAccount(
                InstagramAccountId.New(),
                userId,
                facebookPageId,
                advertisementAccountId,
                metadata
            );
        }

        public void Update(InstagramAccount updatedInstagramAccount)
        {
            this.Metadata = updatedInstagramAccount.Metadata;
        }
    }
}
