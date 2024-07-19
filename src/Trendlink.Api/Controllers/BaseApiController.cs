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
            if (result.IsSuccess)
            {
                return result.Value is not null ? this.Ok(result.Value) : this.NotFound();
            }

            return this.HandleError(result.Error);
        }

        protected IActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
            {
                return this.Ok();
            }

            return this.HandleError(result.Error);
        }

        private IActionResult HandleError(Error error)
        {
            return error switch
            {
                NotFoundError => this.NotFound(error),
                UnauthorizedError => this.Unauthorized(error),
                _ => this.BadRequest(error)
            };
        }
    }
}
