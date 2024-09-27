﻿using Trendlink.Application.Abstractions.Authentication.Models;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceAgePercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceGenderPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceLocationPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceReachPercentage;
using Trendlink.Application.Users.Instagarm.Posts.GetPosts;
using Trendlink.Application.Users.Instagarm.Statistics.GetEngagementStatistics;
using Trendlink.Application.Users.Instagarm.Statistics.GetInteractionStatistics;
using Trendlink.Application.Users.Instagarm.Statistics.GetOverviewStatistics;
using Trendlink.Application.Users.Instagarm.Statistics.GetTableStatistics;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Abstractions.Instagram
{
    public interface IInstagramService
    {
        Task<Result<FacebookTokenResponse>> GetAccessTokenAsync(
            string code,
            CancellationToken cancellationToken = default
        );

        Task<Result<FacebookTokenResponse>> RenewAccessTokenAsync(
            string code,
            CancellationToken cancellationToken = default
        );

        Task<Result<InstagramAccount>> GetInstagramAccountAsync(
            UserId userId,
            string accessToken,
            string facebookPageId,
            string instagramUsername,
            CancellationToken cancellationToken = default
        );

        Task<Result<InstagramAccount>> GetInstagramAccountAsync(
            UserId userId,
            string accessToken,
            CancellationToken cancellationToken = default
        );

        Task<Result<PostsResponse>> GetUserPosts(
            string accessToken,
            string instagramAccountId,
            int limit,
            string cursorType,
            string cursor,
            CancellationToken cancellationToken = default
        );

        Task<Result<AudienceGenderStatistics>> GetAudienceGenderPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        );

        Task<Result<AudienceLocationStatistics>> GetAudienceLocationPercentage(
            string accessToken,
            string instagramAccountId,
            LocationType locationType,
            CancellationToken cancellationToken = default
        );

        Task<Result<AudienceAgeStatistics>> GetAudienceAgePercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        );

        Task<Result<AudienceReachStatistics>> GetAudienceReachPercentage(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        );

        Task<Result<TableStatistics>> GetTableStatistics(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        );

        Task<Result<OverviewStatistics>> GetOverviewStatistics(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        );

        Task<Result<InteractionStatistics>> GetInteractionStatistics(
            string instagramAdAccountId,
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        );

        Task<Result<EngagementStatistics>> GetEngagementStatistics(
            int followersCount,
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        );
    }
}
