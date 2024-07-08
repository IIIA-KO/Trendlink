using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Api.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private ISender _sender;

        protected ISender Sender =>
            this._sender ??= this.HttpContext.RequestServices.GetService<ISender>()!;

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result is null)
            {
                return this.NotFound();
            }

            if (result.IsSuccess && result.Value is not null)
            {
                return this.Ok(result.Value);
            }

            if (result.IsSuccess && result.Value is null)
            {
                return this.NotFound();
            }

            return this.BadRequest(result.Error);
        }

        protected IActionResult HandleResult(Result result)
        {
            if (result is null)
            {
                return this.NotFound();
            }

            if (result.IsSuccess)
            {
                return this.Ok();
            }

            return this.BadRequest(result);
        }
    }
}
