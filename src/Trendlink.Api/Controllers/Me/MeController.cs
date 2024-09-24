using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Advertisements.GetUserAvarageAdvertisementPrices;
using Trendlink.Application.Calendar.GetLoggedInUserCalendar;
using Trendlink.Application.Calendar.GetLoggedInUserCalendarForMonth;
using Trendlink.Application.Conditions.GetUserCondition;
using Trendlink.Application.Notifications.GetLoggedInUserNotifications;
using Trendlink.Application.Users.GetUser;
using Trendlink.Application.Users.Instagarm;
using Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceGenderPercentage;
using Trendlink.Application.Users.Instagarm.Audience.GetUserAudienceReachPercentage;
using Trendlink.Application.Users.Instagarm.Posts.GetPostsTableStatistics;
using Trendlink.Application.Users.Instagarm.Posts.GetUserPosts;
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
            var query = new GetUserPostsQuery(this._userContext.UserId, limit, cursorType, cursor);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("posts-table")]
        public async Task<IActionResult> GetLoggedInUserPostsStatisticsTable(
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetPostsTableStatisticsQuery(this._userContext.UserId, period);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("audience-gender-percentage")]
        public async Task<IActionResult> GetLoggedInUserAudienceGenderPercentage(
            CancellationToken cancellationToken
        )
        {
            var query = new GetUserAudienceGenderPercentageQuery(this._userContext.UserId);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("audience-reach-percentage")]
        public async Task<IActionResult> GetLoggedInUserAudienceReachPercentage(
            [FromQuery] StatisticsPeriod period,
            CancellationToken cancellationToken
        )
        {
            var query = new GetUserAudienceReachPercentageQuery(this._userContext.UserId, period);

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
