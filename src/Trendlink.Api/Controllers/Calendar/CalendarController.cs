using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Calendar.BlockDate;
using Trendlink.Application.Calendar.GetLoggedInUserCooperations;
using Trendlink.Application.Calendar.GetLoggedInUserCooperationsForMonth;
using Trendlink.Application.Calendar.UblockDate;

namespace Trendlink.Api.Controllers.Calendar
{
    [Route("api/calendar")]
    public class CalendarController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetLoggedInUserCooperations(
            CancellationToken cancellationToken
        )
        {
            var query = new GetLoggedInUserCooperationsQuery();

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("month")]
        public async Task<IActionResult> GetLoggedInUserCooperationsForMonth(
            [FromBody] GetLoggedInUserCooperationsForMonthRequest request,
            CancellationToken cancellationToken
        )
        {
            var query = new GetLoggedInUserCooperationsForMonthQuery(request.Month, request.Year);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpPost("block-date")]
        public async Task<IActionResult> BlockDate(
            [FromBody] DateOnly date,
            CancellationToken cancellationToken
        )
        {
            var command = new BlockDateCommand(date);

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpDelete("unblock-date")]
        public async Task<IActionResult> UnblockDate(
            [FromBody] DateOnly date,
            CancellationToken cancellationToken
        )
        {
            var command = new UnblockDateCommand(date);

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }
    }
}
