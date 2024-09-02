using Microsoft.AspNetCore.Mvc;
using Trendlink.Api.Controllers.Calendar;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Calendar.GetLoggedInUserCooperations;
using Trendlink.Application.Calendar.GetLoggedInUserCooperationsForMonth;
using Trendlink.Application.Conditions.GetUserCondition;
using Trendlink.Application.Notifications.GetLoggedInUserNotifications;
using Trendlink.Application.Users.GetUser;
using Trendlink.Application.Users.Instagarm.GetUserPosts;

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
            var query = new GetLoggedInUserCooperationsQuery();

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("calendar/month")]
        public async Task<IActionResult> GetLoggedInUserCooperationsForMonth(
            [FromBody] GetLoggedInUserCooperationsForMonthRequest request,
            CancellationToken cancellationToken
        )
        {
            var query = new GetLoggedInUserCooperationsForMonthQuery(request.Month, request.Year);

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
        public async Task<IActionResult> GetLoggedInUserPosts(CancellationToken cancellationToken)
        {
            var query = new GetUserPostsQuery(this._userContext.UserId);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }
    }
}
