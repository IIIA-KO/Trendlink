using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Users.Instagarm.Statistics.GetOverviewStatistics;
using Trendlink.Application.Users.Instagarm.Statistics.GetTableStatistics;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Infrastructure.Instagram
{
    internal sealed class InstagramStatiscticsService : IInstagramStatisticsService
    {
        private readonly HttpClient _httpClient;
        private readonly InstagramOptions _instagramOptions;

        public InstagramStatiscticsService(
            HttpClient httpClient,
            IOptions<InstagramOptions> instagramOptions
        )
        {
            this._httpClient = httpClient;
            this._instagramOptions = instagramOptions.Value;
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
                $"{this._instagramOptions.BaseUrl}{instagramAccountId}/insights?metric=reach,follower_count,impressions,profile_views"
                + "&period=day"
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
                    var metricData = new TimeSeriesMetricData
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

        public async Task<Result<OverviewStatistics>> GetOverviewStatistics(
            string accessToken,
            string instagramAccountId,
            DateOnly since,
            DateOnly until,
            CancellationToken cancellationToken = default
        )
        {
            string url =
                $"{this._instagramOptions.BaseUrl}{instagramAccountId}/insights?metric=reach,impressions,total_interactions,comments,profile_views,website_clicks"
                + "&metric_type=total_value&period=day"
                + $"&since={since}&until={until}"
                + $"&access_token={accessToken}";

            HttpResponseMessage response = await this.SendGetRequestAsync(url, cancellationToken);

            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            JsonElement jsonObject = JsonDocument.Parse(content).RootElement;

            try
            {
                var overviewStatistics = new OverviewStatistics();

                foreach (
                    JsonElement metricElement in jsonObject.GetProperty("data").EnumerateArray()
                )
                {
                    var metricData = new TotalValueMetricData
                    {
                        Name = metricElement.GetProperty("name").GetString()!,
                        Value = metricElement
                            .GetProperty("total_value")
                            .GetProperty("value")
                            .GetInt32()
                    };

                    overviewStatistics.Metrics.Add(metricData);
                }

                return overviewStatistics;
            }
            catch (Exception)
            {
                return Result.Failure<OverviewStatistics>(Error.Unexpected);
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
