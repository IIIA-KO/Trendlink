using System.Text.Json;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Instagarm.Audience.GetAudienceAgeRatio;
using Trendlink.Application.Instagarm.Audience.GetAudienceLocationPercentage;
using Trendlink.Application.Instagarm.Audience.GetAudienceLocationRatio;
using Trendlink.Application.Instagarm.Audience.GetAudienceReachPercentage;
using Trendlink.Domain.Abstraction;
using Trendlink.Infrastructure.Instagram.Abstraction;
using Trendlink.Infrastructure.Instagram.Models.Audience;
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

        public async Task<Result<GenderRatio>> GetAudienceGenderPercentage(
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

                return new GenderRatio(genderCounts, totalFollowers);
            }
            catch (Exception)
            {
                return Result.Failure<GenderRatio>(Error.Unexpected);
            }
        }

        public async Task<Result<LocationRatio>> GetAudienceTopLocations(
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
                    return Result.Failure<LocationRatio>(Error.NoData);
                }

                var locationPercentages = new List<LocationPercentage>();

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
                        new LocationPercentage { Location = name!, Percentage = percentage }
                    );
                }

                return new LocationRatio(locationPercentages);
            }
            catch (Exception)
            {
                return Result.Failure<LocationRatio>(Error.Unexpected);
            }
        }

        public async Task<Result<AgeRatio>> GetAudienceAgesPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        )
        {
            string url =
                $"{this._instagramOptions.BaseUrl}{instagramAccountId}/insights?"
                + "metric=follower_demographics&period=lifetime&metric_type=total_value&breakdown=age"
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
                    return Result.Failure<AgeRatio>(Error.NoData);
                }

                var agePercentages = new List<AgePercentage>();

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

                    agePercentages.Add(
                        new AgePercentage { AgeGroup = name!, Percentage = percentage }
                    );
                }

                return new AgeRatio(agePercentages);
            }
            catch (Exception)
            {
                return Result.Failure<AgeRatio>(Error.Unexpected);
            }
        }

        public async Task<Result<ReachRatio>> GetAudienceReachPercentage(
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

                return new ReachRatio(followsCounts, totalFollowers);
            }
            catch (Exception)
            {
                return Result.Failure<ReachRatio>(Error.Unexpected);
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
