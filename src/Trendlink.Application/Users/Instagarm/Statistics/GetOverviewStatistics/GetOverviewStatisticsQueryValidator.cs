using FluentValidation;

namespace Trendlink.Application.Users.Instagarm.Statistics.GetOverviewStatistics
{
    internal sealed class GetOverviewStatisticsQueryValidator
        : AbstractValidator<GetOverviewStatisticsQuery>
    {
        public GetOverviewStatisticsQueryValidator()
        {
            this.RuleFor(c => c.StatisticsPeriod).IsInEnum();
        }
    }
}
