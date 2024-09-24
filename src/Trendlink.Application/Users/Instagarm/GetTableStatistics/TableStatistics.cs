namespace Trendlink.Application.Users.Instagarm.GetTableStatistics
{
    public sealed class TableStatistics
    {
        public List<MetricData> Metrics { get; set; } = [];
    }

    public sealed class MetricData
    {
        public string Name { get; set; }

        public Dictionary<DateTime, int> Values { get; set; } = new();
    }
}
