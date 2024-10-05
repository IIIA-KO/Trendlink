using FluentValidation;

namespace Trendlink.Application.Instagarm.Statistics.GetTableStatistics
{
    internal sealed class GetTableStatisticsQueryValidator
        : AbstractValidator<GetTableStatisticsQuery>
    {
        public GetTableStatisticsQueryValidator()
        {
            this.RuleFor(c => c.StatisticsPeriod).IsInEnum();
        }
    }
}
