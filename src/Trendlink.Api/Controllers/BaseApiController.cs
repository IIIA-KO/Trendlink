using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trendlink.Api.Extensions;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Pagination;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Api.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private ISender _sender;

        protected ISender Sender =>
            this._sender ??= this.HttpContext.RequestServices.GetService<ISender>()!;

        private IUserContext _userContext;

        protected IUserContext UserContext =>
            this._userContext ??= this.HttpContext.RequestServices.GetService<IUserContext>()!;

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

        protected IActionResult HandlePagedResult<T>(Result<PagedList<T>> result)
        {
            switch (result.IsSuccess)
            {
                case true when result.Value is not null:
                    this.Response.AddPaginationHeader(
                        result.Value.PageNumber,
                        result.Value.PageSize,
                        result.Value.TotalCount,
                        result.Value.TotalPages,
                        result.Value.HasNextPage,
                        result.Value.HasPreviousPage
                    );
                    return this.Ok(result.Value);

                default:
                    return this.HandleError(result.Error);
            }
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
