using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Instagarm.Statistics.GetEngagementStatistics;
using Trendlink.Application.Instagarm.Statistics.GetInteractionStatistics;
using Trendlink.Application.Instagarm.Statistics.GetOverviewStatistics;
using Trendlink.Application.Instagarm.Statistics.GetTableStatistics;
using Trendlink.Domain.Abstraction;
using Trendlink.Infrastructure.Instagram.Abstraction;

namespace Trendlink.Infrastructure.Instagram
{
    internal sealed class InstagramStatiscticsService
        : InstagramBaseService,
            IInstagramStatisticsService
    {
        public InstagramStatiscticsService(
            HttpClient httpClient,
            IOptions<InstagramOptions> instagramOptions
        )
            : base(httpClient, instagramOptions) { }

        public async Task<Result<TableStatistics>> GetTableStatistics(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "metric", "reach,follower_count,impressions,profile_views" },
                { "period", "day" },
                { "since", request.Since.ToString(CultureInfo.InvariantCulture) },
                { "until", request.Until.ToString(CultureInfo.InvariantCulture) },
                { "access_token", request.AccessToken }
            };

            string url = this.BuildUrl($"{request.InstagramAccountId}/insights", parameters);

            JsonElement response = await this.GetAsync(url, cancellationToken);

            return response.ValueKind != JsonValueKind.Undefined
                ? ParseTableStatistics(response)
                : Result.Failure<TableStatistics>(Error.Unexpected);
        }

        public async Task<Result<OverviewStatistics>> GetOverviewStatistics(
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var parameters = new Dictionary<string, string>
            {
                {
                    "metric",
                    "reach,impressions,total_interactions,comments,profile_views,website_clicks"
                },
                { "metric_type", "total_value" },
                { "period", "day" },
                { "since", request.Since.ToString(CultureInfo.InvariantCulture) },
                { "until", request.Until.ToString(CultureInfo.InvariantCulture) },
                { "access_token", request.AccessToken }
            };

            string url = this.BuildUrl($"{request.InstagramAccountId}/insights", parameters);

            JsonElement response = await this.GetAsync(url, cancellationToken);

            return response.ValueKind != JsonValueKind.Undefined
                ? ParseOverviewStatistics(response)
                : Result.Failure<OverviewStatistics>(Error.Unexpected);
        }

        public async Task<Result<InteractionStatistics>> GetInteractionsStatistics(
            string instagramAdAccountId,
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var interactionParameters = new Dictionary<string, string>
            {
                { "metric", "total_interactions,reach,likes,comments" },
                { "metric_type", "total_value" },
                { "period", "day" },
                { "since", request.Since.ToString(CultureInfo.InvariantCulture) },
                { "until", request.Until.ToString(CultureInfo.InvariantCulture) },
                { "access_token", request.AccessToken }
            };

            string interactionUrl = this.BuildUrl(
                $"{request.InstagramAccountId}/insights",
                interactionParameters
            );

            var adsParameters = new Dictionary<string, string>
            {
                { "fields", "spend" },
                { "since", request.Since.ToString(CultureInfo.InvariantCulture) },
                { "until", request.Until.ToString(CultureInfo.InvariantCulture) },
                { "access_token", request.AccessToken }
            };

            string adsUrl = this.BuildUrl($"{instagramAdAccountId}/insights", adsParameters);

            JsonElement inteactionResponse = await this.GetAsync(interactionUrl, cancellationToken);

            JsonElement adsResponse = await this.GetAsync(adsUrl, cancellationToken);

            int daysCount = request.Until.DayNumber - request.Since.DayNumber + 1;

            return inteactionResponse.ValueKind != JsonValueKind.Undefined
                ? ParseInteractionStatistics(inteactionResponse, adsResponse, daysCount)
                : Result.Failure<InteractionStatistics>(Error.Unexpected);
        }

        public async Task<Result<EngagementStatistics>> GetEngagementStatistics(
            int followersCount,
            InstagramPeriodRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "metric", "reach,impressions,profile_views,likes,comments,saves" },
                { "metric_type", "total_value" },
                { "period", "day" },
                { "since", request.Since.ToString(CultureInfo.InvariantCulture) },
                { "until", request.Until.ToString(CultureInfo.InvariantCulture) },
                { "access_token", request.AccessToken }
            };

            string url = this.BuildUrl($"{request.InstagramAccountId}/insights", parameters);

            JsonElement response = await this.GetAsync(url, cancellationToken);

            return response.ValueKind != JsonValueKind.Undefined
                ? ParseEngagementsStatistics(response, followersCount)
                : Result.Failure<EngagementStatistics>(Error.Unexpected);
        }

        private static Result<TableStatistics> ParseTableStatistics(JsonElement response)
        {
            if (!response.GetProperty("data").EnumerateArray().Any())
            {
                return Result.Failure<TableStatistics>(Error.NoData);
            }

            var postsTableStatistics = new TableStatistics();

            foreach (JsonElement metricElement in response.GetProperty("data").EnumerateArray())
            {
                var metricData = new TimeSeriesMetricData
                {
                    Name = metricElement.GetProperty("name").GetString()!
                };

                foreach (
                    JsonElement valueElement in metricElement.GetProperty("values").EnumerateArray()
                )
                {
                    int metricValue = valueElement.GetProperty("value").GetInt32();

                    DateTime endTime = ParseDateTime(
                        valueElement.GetProperty("end_time").GetString()!
                    );

                    metricData.Values.Add(endTime, metricValue);
                }

                postsTableStatistics.Metrics.Add(metricData);
            }

            return postsTableStatistics;
        }

        private static DateTime ParseDateTime(string dateTimeString)
        {
            return DateTimeOffset
                .ParseExact(dateTimeString, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture)
                .UtcDateTime;
        }

        private static Result<OverviewStatistics> ParseOverviewStatistics(JsonElement response)
        {
            if (!response.GetProperty("data").EnumerateArray().Any())
            {
                return Result.Failure<OverviewStatistics>(Error.NoData);
            }

            var overviewStatistics = new OverviewStatistics();

            foreach (JsonElement metricElement in response.GetProperty("data").EnumerateArray())
            {
                var metricData = new TotalValueMetricData
                {
                    Name = metricElement.GetProperty("name").GetString()!,
                    Value = metricElement.GetProperty("total_value").GetProperty("value").GetInt32()
                };

                overviewStatistics.Metrics.Add(metricData);
            }

            return overviewStatistics;
        }

        private static Result<InteractionStatistics> ParseInteractionStatistics(
            JsonElement interactionResponse,
            JsonElement adsResponse,
            int daysCount
        )
        {
            if (
                !interactionResponse.GetProperty("data").EnumerateArray().Any()
                || !adsResponse.GetProperty("data").EnumerateArray().Any()
            )
            {
                return Result.Failure<InteractionStatistics>(Error.NoData);
            }

            int interactions = ParseMetricTotalValue(interactionResponse, "total_interactions");
            int reach = ParseMetricTotalValue(interactionResponse, "reach");
            int likes = ParseMetricTotalValue(interactionResponse, "likes");
            int comments = ParseMetricTotalValue(interactionResponse, "comments");

            double engagementRate = (double)interactions / reach * 100;

            double averageLikes = (double)likes / daysCount;

            double averageComments = (double)comments / daysCount;

            double totalAdSpend = 0;

            if (adsResponse.GetProperty("data").EnumerateArray().Any())
            {
                totalAdSpend = double.Parse(
                    adsResponse.GetProperty("data")[0].GetProperty("spend").GetString()!,
                    CultureInfo.InvariantCulture
                );
            }

            double cpe = 0;
            if (interactions > 0)
            {
                cpe = totalAdSpend / interactions;
            }

            return new InteractionStatistics(engagementRate, averageLikes, averageComments, cpe);
        }

        private static Result<EngagementStatistics> ParseEngagementsStatistics(
            JsonElement response,
            int followersCount
        )
        {
            if (!response.GetProperty("data").EnumerateArray().Any())
            {
                return Result.Failure<EngagementStatistics>(Error.NoData);
            }

            double reach = ParseMetricTotalValue(response, "reach");
            double likes = ParseMetricTotalValue(response, "likes");
            double comments = ParseMetricTotalValue(response, "comments");
            double saves = ParseMetricTotalValue(response, "saves");

            double totalEngagements = likes + comments + saves;

            double reachRate = reach / followersCount * 100;

            double engagementRate = totalEngagements / followersCount * 100;

            double erReach = totalEngagements / reach * 100;

            return new EngagementStatistics(reachRate, engagementRate, erReach);
        }

        private static int ParseMetricTotalValue(JsonElement response, string metric)
        {
            return response
                .GetProperty("data")
                .EnumerateArray()
                .First(e => e.GetProperty("name").GetString() == metric)
                .GetProperty("total_value")
                .GetProperty("value")
                .GetInt32();
        }
    }
}
