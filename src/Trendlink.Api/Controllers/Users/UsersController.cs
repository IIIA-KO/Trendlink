using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Users.RegisterUser;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.Cities;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Api.Controllers.Users
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            this._sender = sender;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new RegisterUserCommand(
                request.FirstName,
                request.LastName,
                request.BirthDate,
                request.Email,
                request.PhoneNumber,
                request.Password,
                new CityId(request.CityId)
            );

            Result<UserId> result = await this._sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return this.BadRequest(result.Error);
            }

            return this.Ok(result.Value);
        }
    }
}
