namespace Trendlink.Application.Users.Instagarm.Statistics.GetTableStatistics
{
    public sealed class TimeSeriesMetricData
    {
        public string Name { get; set; }

        public Dictionary<DateTime, int> Values { get; set; } = [];
    }
}
