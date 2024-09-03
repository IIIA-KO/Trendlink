﻿using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Calendar.BlockDate;
using Trendlink.Application.Calendar.GetUserCalendar;
using Trendlink.Application.Calendar.GetUserCalendarForMonth;
using Trendlink.Application.Calendar.UblockDate;
using Trendlink.Domain.Users;

namespace Trendlink.Api.Controllers.Calendar
{
    [Route("api/calendar")]
    public class CalendarController : BaseApiController
    {
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUserCalendar(
            [FromRoute] Guid userId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetUserCalendarQuery(new UserId(userId));

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("{userId:guid}/month")]
        public async Task<IActionResult> GetUserCalendarForMonth(
            [FromRoute] Guid userId,
            [FromBody] GetLoggedInUserCooperationsForMonthRequest request,
            CancellationToken cancellationToken
        )
        {
            var query = new GetUserCalendarForMonthQuery(
                new UserId(userId),
                request.Month,
                request.Year
            );
            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpPost("block-date")]
        public async Task<IActionResult> BlockDate(
            [FromBody] BlockDateRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new BlockDateCommand(request.Date);

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpDelete("unblock-date")]
        public async Task<IActionResult> UnblockDate(
            [FromBody] UnblockDateRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new UnblockDateCommand(request.Date);

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }
    }
}
