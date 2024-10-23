namespace Trendlink.Application.Instagarm.Statistics.GetInteractionStatistics
{
    public sealed record InteractionStatistics(
        double EngagementRate,
        double AverageLikes,
        double AverageComments,
        double CPE
    );
}
