using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Users.Instagarm;
using Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceGenderPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceReachPercentage;
using Trendlink.Application.Users.Instagarm.Posts.GetPostsTableStatistics;
using Trendlink.Application.Users.Instagarm.Posts.GetUserPosts;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Abstractions.Instagram
{
    public interface IInstagramService
    {
        Task<FacebookTokenResponse?> GetAccessTokenAsync(
            string code,
            CancellationToken cancellationToken = default
        );

        Task<FacebookTokenResponse?> RenewAccessTokenAsync(
            string code,
            CancellationToken cancellationToken = default
        );

        Task<Result<InstagramUserInfo>> GetUserInfoAsync(
            string accessToken,
            string facebookPageId,
            string instagramUsername,
            CancellationToken cancellationToken = default
        );

        Task<Result<InstagramUserInfo>> GetUserInfoAsync(
            string accessToken,
            CancellationToken cancellationToken = default
        );

        Task<Result<UserPostsResponse>> GetUserPosts(
            string accessToken,
            string instagramAccountId,
            int limit,
            string cursorType,
            string cursor,
            CancellationToken cancellationToken = default
        );

        Task<Result<PostsTableStatistics>> GetPostsTable(
            string accessToken,
            string instagramAccountId,
            StatisticsPeriod statisticsPeriod,
            CancellationToken cancellationToken = default
        );

        Task<Result<AudienceGenderStatistics>> GetUserAudienceGenderPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        );

        Task<Result<AudienceReachStatistics>> GetUserAudienceReachPercentage(
            string accessToken,
            string instagramAccountId,
            StatisticsPeriod statisticsPeriod,
            CancellationToken cancellationToken = default
        );
    }
}
