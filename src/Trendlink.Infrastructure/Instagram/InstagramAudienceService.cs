﻿using System.Text.Json;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceGenderPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceReachPercentage;
using Trendlink.Domain.Abstraction;
using static System.Text.Json.JsonElement;

namespace Trendlink.Infrastructure.Instagram
{
    internal sealed class InstagramAudienceService : IInstagramAudienceService
    {
        private readonly HttpClient _httpClient;
        private readonly InstagramOptions _instagramOptions;

        public InstagramAudienceService(
            HttpClient httpClient,
            IOptions<InstagramOptions> instagramOptions
        )
        {
            this._httpClient = httpClient;
            this._instagramOptions = instagramOptions.Value;
        }

        public async Task<Result<AudienceGenderStatistics>> GetUserAudienceGenderPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        )
        {
            string url =
                $"{this._instagramOptions.BaseUrl}{instagramAccountId}/insights?metric=follower_demographics&period=lifetime&metric_type=total_value&breakdown=gender&access_token={accessToken}";

            HttpResponseMessage response = await this.SendGetRequestAsync(url, cancellationToken);

            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            JsonElement jsonObject = JsonDocument.Parse(content).RootElement;

            try
            {
                ArrayEnumerator results = jsonObject
                    .GetProperty("data")[0]
                    .GetProperty("total_value")
                    .GetProperty("breakdowns")[0]
                    .GetProperty("results")
                    .EnumerateArray();

                int totalFollowers = 0;
                var genderCounts = new Dictionary<string, int>();

                foreach (JsonElement result in results)
                {
                    string gender = result.GetProperty("dimension_values")[0].GetString();
                    int count = result.GetProperty("value").GetInt32();

                    totalFollowers += count;

                    if (genderCounts.ContainsKey(gender!))
                    {
                        genderCounts[gender!] += count;
                    }
                    else
                    {
                        genderCounts[gender!] = count;
                    }
                }

                var genderPercentages = genderCounts
                    .Select(g => new AudienceGenderPercentageResponse
                    {
                        Gender = g.Key switch
                        {
                            "F" => "Female",
                            "M" => "Male",
                            _ => "Unknown"
                        },
                        Percentage = (double)g.Value / totalFollowers * 100
                    })
                    .ToList();

                return new AudienceGenderStatistics(genderPercentages);
            }
            catch (Exception)
            {
                return Result.Failure<AudienceGenderStatistics>(Error.Unexpected);
            }
        }

        public async Task<Result<AudienceReachStatistics>> GetUserAudienceReachPercentage(
            string accessToken,
            string instagramAccountId,
            DateOnly since,
            DateOnly until,
            CancellationToken cancellationToken = default
        )
        {
            string url =
                $"{this._instagramOptions.BaseUrl}{instagramAccountId}/insights?metric=reach&period=day"
                + $"&since={since}&until={until}"
                + "&breakdown=follow_type&metric_type=total_value"
                + $"&access_token={accessToken}";

            HttpResponseMessage response = await this.SendGetRequestAsync(url, cancellationToken);

            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            JsonElement jsonObject = JsonDocument.Parse(content).RootElement;

            try
            {
                ArrayEnumerator results = jsonObject
                    .GetProperty("data")[0]
                    .GetProperty("total_value")
                    .GetProperty("breakdowns")[0]
                    .GetProperty("results")
                    .EnumerateArray();

                int totalFollowers = 0;
                var followsCounts = new Dictionary<string, int>();

                foreach (JsonElement result in results)
                {
                    string followType = result.GetProperty("dimension_values")[0].GetString();
                    int count = result.GetProperty("value").GetInt32();

                    totalFollowers += count;

                    if (followsCounts.ContainsKey(followType!))
                    {
                        followsCounts[followType!] += count;
                    }
                    else
                    {
                        followsCounts[followType!] = count;
                    }
                }

                var reachPercentages = followsCounts
                    .Select(g => new AudienceReachPercentageResponse
                    {
                        FollowType = g.Key switch
                        {
                            "FOLLOWER" => "Follower",
                            _ => "NonFollower"
                        },
                        Percentage = (double)g.Value / totalFollowers * 100
                    })
                    .ToList();

                return new AudienceReachStatistics(reachPercentages);
            }
            catch (Exception)
            {
                return Result.Failure<AudienceReachStatistics>(Error.Unexpected);
            }
        }

        private async Task<HttpResponseMessage> SendGetRequestAsync(
            string url,
            CancellationToken cancellationToken
        )
        {
            return await this._httpClient.GetAsync(url, cancellationToken);
        }
    }
}