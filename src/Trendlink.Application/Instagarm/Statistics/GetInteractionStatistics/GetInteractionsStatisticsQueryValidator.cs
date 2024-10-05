using FluentValidation;

namespace Trendlink.Application.Instagarm.Statistics.GetInteractionStatistics
{
    internal sealed class GetInteractionsStatisticsQueryValidator
        : AbstractValidator<GetInteractionStatisticsQuery>
    {
        public GetInteractionsStatisticsQueryValidator()
        {
            this.RuleFor(c => c.StatisticsPeriod).IsInEnum();
        }
    }
}
