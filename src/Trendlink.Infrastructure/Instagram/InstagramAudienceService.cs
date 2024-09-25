using System.Text.Json;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceGenderPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceLocationPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceReachPercentage;
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

        public async Task<Result<AudienceGenderStatistics>> GetAudienceGenderPercentage(
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

        public async Task<Result<AudienceLocationStatistics>> GetAudienceTopLocations(
            string accessToken,
            string instagramAccountId,
            LocationType locationType,
            CancellationToken cancellationToken = default
        )
        {
            string breakdownValue = locationType switch
            {
                LocationType.City => "city",
                LocationType.Country => "country",
                _ => throw new ArgumentOutOfRangeException(nameof(locationType), locationType, null)
            };

            string url =
                $"{this._instagramOptions.BaseUrl}{instagramAccountId}/insights?metric=follower_demographics&period=lifetime"
                + "&metric_type=total_value"
                + $"&breakdown={breakdownValue}"
                + $"&access_token={accessToken}";

            HttpResponseMessage response = await this.SendGetRequestAsync(url, cancellationToken);

            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            JsonElement jsonObject = JsonDocument.Parse(content).RootElement;

            try
            {
                if (
                    !jsonObject.TryGetProperty("data", out JsonElement dataElement)
                    || dataElement.GetArrayLength() == 0
                )
                {
                    return Result.Failure<AudienceLocationStatistics>(Error.NoData);
                }

                var locationPercentages = new List<AudienceLocationPercentageResponse>();

                JsonElement results = dataElement[0]
                    .GetProperty("total_value")
                    .GetProperty("breakdowns")[0]
                    .GetProperty("results");

                double totalValue = results
                    .EnumerateArray()
                    .Sum(result => result.GetProperty("value").GetDouble());

                foreach (JsonElement result in results.EnumerateArray())
                {
                    string name = result.GetProperty("dimension_values")[0].GetString();
                    double value = result.GetProperty("value").GetDouble();

                    double percentage = value / totalValue * 100;

                    locationPercentages.Add(
                        new AudienceLocationPercentageResponse
                        {
                            Name = name!,
                            Percentage = percentage
                        }
                    );
                }

                var sortedLocations = locationPercentages
                    .OrderByDescending(l => l.Percentage)
                    .ToList();

                var topLocations = sortedLocations.Take(4).ToList();

                double otherPercentage = sortedLocations.Skip(4).Sum(l => l.Percentage);

                if (otherPercentage > 0)
                {
                    topLocations.Add(
                        new AudienceLocationPercentageResponse
                        {
                            Name = "Other",
                            Percentage = otherPercentage
                        }
                    );
                }

                return new AudienceLocationStatistics(topLocations);
            }
            catch (Exception)
            {
                return Result.Failure<AudienceLocationStatistics>(Error.Unexpected);
            }
        }

        public async Task<Result<AudienceReachStatistics>> GetAudienceReachPercentage(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            string url =
                $"{this._instagramOptions.BaseUrl}{request.InstagramAccountId}/insights?metric=reach&period=day"
                + $"&since={request.Since}&until={request.Until}"
                + "&breakdown=follow_type&metric_type=total_value"
                + $"&access_token={request.AccessToken}";

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

        public Task<Result<string>> GetAudienceAgesPercentage(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            throw new NotImplementedException();
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
