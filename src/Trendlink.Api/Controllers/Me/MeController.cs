using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Advertisements.GetUserAvarageAdvertisementPrices;
using Trendlink.Application.Calendar.GetLoggedInUserCalendar;
using Trendlink.Application.Calendar.GetLoggedInUserCalendarForMonth;
using Trendlink.Application.Conditions.GetUserCondition;
using Trendlink.Application.Notifications.GetLoggedInUserNotifications;
using Trendlink.Application.Users.DeleteUserAccount;
using Trendlink.Application.Users.GetUser;
using Trendlink.Application.Users.Instagarm;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceAgePercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceGenderPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceLocationPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetAudienceReachPercentage;
using Trendlink.Application.Users.Instagarm.Posts.GetPosts;
using Trendlink.Application.Users.Instagarm.Statistics.GetEngagementStatistics;
using Trendlink.Application.Users.Instagarm.Statistics.GetInteractionStatistics;
using Trendlink.Application.Users.Instagarm.Statistics.GetOverviewStatistics;
using Trendlink.Application.Users.Instagarm.Statistics.GetTableStatistics;
using Trendlink.Application.Users.Photos.DeleteProfilePhoto;
using Trendlink.Application.Users.Photos.SetProfilePhoto;

namespace Trendlink.Api.Controllers.Me
{
    [Route("/api/me")]
    public class MeController : BaseApiController
    {
        private readonly IUserContext _userContext;

        public MeController(IUserContext userContext)
        {
            this._userContext = userContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetLoggedInUser(CancellationToken cancellationToken)
        {
            var query = new GetUserQuery(this._userContext.UserId);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAccount(CancellationToken cancellationToken)
        {
            var command = new DeleteUserAccountCommand();

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpGet("calendar")]
        public async Task<IActionResult> GetLoggedInUserCooperations(
            CancellationToken cancellationToken
        )
        {
            var query = new GetLoggedInUserCalendarQuery();

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("calendar/month")]
        public async Task<IActionResult> GetLoggedInUserCooperationsForMonth(
            [FromBody] GetLoggedInUserCalendarForMonthRequest request,
            CancellationToken cancellationToken
        )
        {
            var query = new GetLoggedInUserCalendarForMonthQuery(request.Month, request.Year);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("terms-and-conditions")]
        public async Task<IActionResult> GetLoggedInUserConditions(
            CancellationToken cancellationToken
        )
        {
            var query = new GetUserConditionQuery(this._userContext.UserId);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("notifications")]
        public async Task<IActionResult> GetLoggedInUserNotifications(
            [FromQuery] string? sortColumn,
            [FromQuery] string? sortOrder,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default
        )
        {
            var query = new GetLoggedInUserNotificationsQuery(
                sortColumn,
                sortOrder,
                pageNumber,
                pageSize
            );

            return this.HandlePagedResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("posts")]
        public async Task<IActionResult> GetLoggedInUserPosts(
            [FromQuery] int limit,
            [FromQuery] string? cursorType,
            [FromQuery] string? cursor,
            CancellationToken cancellationToken
        )
        {
            var query = new GetPostsQuery(this._userContext.UserId, limit, cursorType, cursor);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
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

        [HttpGet("overview")]
        public async Task<IActionResult> GetLoggedInUserOverviewStatistics(
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetOverviewStatisticsQuery(this._userContext.UserId, period);

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

        [HttpGet("engagement")]
        public async Task<IActionResult> GetLoggedInUserEngagementStatistics(
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetEngagementStatisticsQuery(this._userContext.UserId, period);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("audience-gender-percentage")]
        public async Task<IActionResult> GetLoggedInUserAudienceGenderPercentage(
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceGenderPercentageQuery(this._userContext.UserId);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("audience-reach-percentage")]
        public async Task<IActionResult> GetLoggedInUserAudienceReachPercentage(
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceReachPercentageQuery(this._userContext.UserId, period);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("audience-location-percentage")]
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

        [HttpGet("audience-age-percentage")]
        public async Task<IActionResult> GetLoggedInUserAudienceAgePercentage(
            CancellationToken cancellationToken
        )
        {
            var query = new GetAudienceAgePercentageQuery(this._userContext.UserId);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("avarage-prices")]
        public async Task<IActionResult> GetUserAvarageAdvertisementPrices(
            CancellationToken cancellationToken
        )
        {
            var query = new GetUserAvarageAdvertisementPricesQuery(this._userContext.UserId);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpPost("profile-photo")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> SetProfilePhoto(
            [FromForm] IFormFile photo,
            CancellationToken cancellationToken
        )
        {
            var command = new SetProfilePhotoCommand(photo);

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpDelete("profile-photo")]
        public async Task<IActionResult> DeleteProfilePhoto(CancellationToken cancellationToken)
        {
            var command = new DeleteProfilePhotoCommand();

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }
    }
}
