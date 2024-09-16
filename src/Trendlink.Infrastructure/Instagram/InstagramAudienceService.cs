using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Trendlink.Application.Users.Instagarm.GetUserAudienceGenderPercentage;
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

        public async Task<List<AudienceGenderPercentageResponse>> GetUserAudienceGenderPercentage(
            string accessToken,
            string instagramAccountId,
            CancellationToken cancellationToken = default
        )
        {
            string audienceUrl =
                $"{this._instagramOptions.BaseUrl}{instagramAccountId}/insights?metric=follower_demographics&period=lifetime&metric_type=total_value&breakdown=gender&access_token={accessToken}";

            HttpResponseMessage response = await this.SendGetRequestAsync(
                audienceUrl,
                cancellationToken
            );

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

            return genderCounts
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
