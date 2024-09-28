using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Instagarm;
using Trendlink.Application.Instagarm.Statistics.GetEngagementStatistics;
using Trendlink.Application.Instagarm.Statistics.GetInteractionStatistics;
using Trendlink.Application.Instagarm.Statistics.GetOverviewStatistics;
using Trendlink.Application.Instagarm.Statistics.GetTableStatistics;
using Trendlink.Domain.Users;

namespace Trendlink.Api.Controllers.Instagram
{
    [Route("/api/statistics")]
    public class InstagramStatisticsController : BaseApiController
    {
        private readonly IUserContext _userContext;

        public InstagramStatisticsController(IUserContext userContext)
        {
            this._userContext = userContext;
        }

        [HttpGet("table")]
        public async Task<IActionResult> GetLoggedInUserTableStatistics(
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetTableStatisticsQuery(this._userContext.UserId, period);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("table/{userId:guid}")]
        public async Task<IActionResult> GetLoggedInUserTableStatistics(
            Guid userId,
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetTableStatisticsQuery(new UserId(userId), period);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("overview")]
        public async Task<IActionResult> GetLoggedInUserOverviewStatistics(
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetOverviewStatisticsQuery(this._userContext.UserId, period);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("overview/{userId:guid}")]
        public async Task<IActionResult> GetLoggedInUserOverviewStatistics(
            Guid userId,
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetOverviewStatisticsQuery(new UserId(userId), period);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("interaction")]
        public async Task<IActionResult> GetLoggedInUserInteractionStatistics(
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetInteractionStatisticsQuery(this._userContext.UserId, period);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("interaction/{userId:guid}")]
        public async Task<IActionResult> GetLoggedInUserInteractionStatistics(
            Guid userId,
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetInteractionStatisticsQuery(new UserId(userId), period);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("engagement")]
        public async Task<IActionResult> GetLoggedInUserEngagementStatistics(
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetEngagementStatisticsQuery(this._userContext.UserId, period);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("engagement/{userId:guid}")]
        public async Task<IActionResult> GetLoggedInUserEngagementStatistics(
            Guid userId,
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetEngagementStatisticsQuery(new UserId(userId), period);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }
    }
}
