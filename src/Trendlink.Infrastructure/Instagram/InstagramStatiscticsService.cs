using System.Globalization;
using System.Text.Json;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Users.Instagarm.GetTableStatistics;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Infrastructure.Instagram
{
    internal sealed class InstagramStatiscticsService : IInstagramStatisticsService
    {
        private readonly HttpClient _httpClient;
        private readonly InstagramOptions _instagramOptions;

        public InstagramStatiscticsService(HttpClient httpClient, InstagramOptions instagramOptions)
        {
            this._httpClient = httpClient;
            this._instagramOptions = instagramOptions;
        }

        public async Task<Result<TableStatistics>> GetTableStatistics(
            string accessToken,
            string instagramAccountId,
            DateOnly since,
            DateOnly until,
            CancellationToken cancellationToken = default
        )
        {
            string url =
                $"{this._instagramOptions.BaseUrl}{instagramAccountId}/insights?metric=reach,follower_count,impressions,profile_views&period=day"
                + $"&since={since}&until={until}"
                + $"&access_token={accessToken}";

            HttpResponseMessage response = await this.SendGetRequestAsync(url, cancellationToken);

            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            JsonElement jsonObject = JsonDocument.Parse(content).RootElement;

            try
            {
                var postsTableStatistics = new TableStatistics();

                foreach (
                    JsonElement metricElement in jsonObject.GetProperty("data").EnumerateArray()
                )
                {
                    var metricData = new MetricData
                    {
                        Name = metricElement.GetProperty("name").GetString()!
                    };

                    foreach (
                        JsonElement valueElement in metricElement
                            .GetProperty("values")
                            .EnumerateArray()
                    )
                    {
                        int metricValue = valueElement.GetProperty("value").GetInt32();

                        string endTimeString = valueElement.GetProperty("end_time").GetString()!;

                        DateTime endTime = DateTimeOffset
                            .ParseExact(
                                endTimeString,
                                "yyyy-MM-ddTHH:mm:sszzz",
                                CultureInfo.InvariantCulture
                            )
                            .UtcDateTime;

                        metricData.Values.Add(endTime, metricValue);
                    }

                    postsTableStatistics.Metrics.Add(metricData);
                }

                return postsTableStatistics;
            }
            catch (Exception)
            {
                return Result.Failure<TableStatistics>(Error.Unexpected);
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
