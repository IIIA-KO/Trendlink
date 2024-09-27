using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Users.Instagarm;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceAgePercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceGenderPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceLocationPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceReachPercentage;
using Trendlink.Domain.Users;

namespace Trendlink.Api.Controllers.Instagram
{
    [Route("audience")]
    public class InstagramAudienceController : BaseApiController
    {
        private readonly IUserContext _userContext;

        public InstagramAudienceController(IUserContext userContext)
        {
            this._userContext = userContext;
        }

        [HttpGet("gender")]
        public async Task<IActionResult> GetLoggedInUserAudienceGenderPercentage(
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceGenderPercentageQuery(this._userContext.UserId);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("gender/{userId:guid}")]
        public async Task<IActionResult> GetLoggedInUserAudienceGenderPercentage(
            Guid userId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceGenderPercentageQuery(new UserId(userId));

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("reach")]
        public async Task<IActionResult> GetLoggedInUserAudienceReachPercentage(
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceReachPercentageQuery(this._userContext.UserId, period);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("reach/{userId:guid}")]
        public async Task<IActionResult> GetLoggedInUserAudienceReachPercentage(
            Guid userId,
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceReachPercentageQuery(new UserId(userId), period);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("location")]
        public async Task<IActionResult> GetLoggedInUserAudienceLocationPercentage(
            [FromQuery] LocationType locationType,
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceLocationPercentageQuery(
                this._userContext.UserId,
                locationType
            );

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("location/{userId:guid}")]
        public async Task<IActionResult> GetLoggedInUserAudienceLocationPercentage(
            Guid userId,
            [FromQuery] LocationType locationType,
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceLocationPercentageQuery(new UserId(userId), locationType);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("age")]
        public async Task<IActionResult> GetLoggedInUserAudienceAgePercentage(
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceAgePercentageQuery(this._userContext.UserId);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("age/{userId:guid}")]
        public async Task<IActionResult> GetLoggedInUserAudienceAgePercentage(
            Guid userId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceAgePercentageQuery(new UserId(userId));

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }
    }
}
