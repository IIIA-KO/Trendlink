using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Instagarm;
using Trendlink.Application.Instagarm.Audience.GetAudienceAgeRatio;
using Trendlink.Application.Instagarm.Audience.GetAudienceGenderRatio;
using Trendlink.Application.Instagarm.Audience.GetAudienceLocationRatio;
using Trendlink.Application.Instagarm.Audience.GetAudienceReachRatio;
using Trendlink.Domain.Users;

namespace Trendlink.Api.Controllers.Instagram
{
    [Route("/api/audience")]
    public class InstagramAudienceController : BaseApiController
    {
        private readonly IUserContext _userContext;

        public InstagramAudienceController(IUserContext userContext)
        {
            this._userContext = userContext;
        }

        [HttpGet("gender")]
        public async Task<IActionResult> GetLoggedInUserAudienceGenderRatio(
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceGenderRatioQuery(this._userContext.UserId);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("gender/{userId:guid}")]
        public async Task<IActionResult> GetUserAudienceGenderRatio(
            Guid userId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceGenderRatioQuery(new UserId(userId));

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("reach")]
        public async Task<IActionResult> GetLoggedInUserAudienceReachPercentage(
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceReachRatioQuery(this._userContext.UserId, period);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("reach/{userId:guid}")]
        public async Task<IActionResult> GetUserAudienceReachPercentage(
            Guid userId,
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceReachRatioQuery(new UserId(userId), period);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("location")]
        public async Task<IActionResult> GetLoggedInUserAudienceLocationPercentage(
            [FromQuery] LocationType locationType,
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceLocationRatioQuery(this._userContext.UserId, locationType);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("location/{userId:guid}")]
        public async Task<IActionResult> GetUserAudienceLocationPercentage(
            Guid userId,
            [FromQuery] LocationType locationType,
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceLocationRatioQuery(new UserId(userId), locationType);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("age")]
        public async Task<IActionResult> GetLoggedInUserAudienceAgePercentage(
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceAgeRatioQuery(this._userContext.UserId);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("age/{userId:guid}")]
        public async Task<IActionResult> GetUserAudienceAgePercentage(
            Guid userId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceAgeRatioQuery(new UserId(userId));

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }
    }
}
