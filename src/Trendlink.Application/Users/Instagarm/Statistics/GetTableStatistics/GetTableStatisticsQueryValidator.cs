using FluentValidation;

namespace Trendlink.Application.Users.Instagarm.Statistics.GetTableStatistics
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
