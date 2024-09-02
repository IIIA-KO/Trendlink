using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Calendar.BlockDate;
using Trendlink.Application.Calendar.UblockDate;

namespace Trendlink.Api.Controllers.Calendar
{
    [Route("api/calendar")]
    public class CalendarController : BaseApiController
    {
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
