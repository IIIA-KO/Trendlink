using System.Text.Json;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceGenderPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceReachPercentage;
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

        public async Task<AudienceGenderStatistics> GetUserAudienceGenderPercentage(
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

            return new AudienceGenderStatistics(genderCounts, totalFollowers);
        }

        public async Task<AudienceReachStatistics> GetUserAudienceReachPercentage(
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

            return new AudienceReachStatistics(followsCounts, totalFollowers);
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
