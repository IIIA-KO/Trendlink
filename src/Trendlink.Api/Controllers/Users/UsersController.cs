using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Users.EditUser;
using Trendlink.Application.Users.GetLoggedInUser;
using Trendlink.Application.Users.LogInUser;
using Trendlink.Application.Users.RefreshToken;
using Trendlink.Application.Users.RegisterUser;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Api.Controllers.Users
{
    [Route("/api/users")]
    public class UsersController : BaseApiController
    {
        [HttpGet("me")]
        public async Task<IActionResult> GetLoggedInUse(CancellationToken cancellationToken)
        {
            var query = new GetLoggedInUserQuery();

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserRequest request,
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
                new StateId(request.StateId)
            );

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LogIn(
            [FromBody] LogInUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new LogInUserCommand(request.Email, request.Password);

            Result<AccessTokenResponse> result = await this.Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return this.Unauthorized(result.Error);
            }

            return this.Ok(result.Value);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(
            [FromBody] RefreshTokenRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new RefreshTokenCommand(request.RefreshToken);

            Result<AccessTokenResponse> result = await this.Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return this.Unauthorized(result.Error);
            }

            return this.Ok(result.Value);
        }

        [HttpPut("edit/{id:guid}")]
        public async Task<IActionResult> EditUser(
            Guid id,
            [FromBody] EditUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new EditUserCommand(
                new UserId(id),
                new FirstName(request.FirstName),
                new LastName(request.LastName),
                request.BirthDate,
                new StateId(request.StateId),
                new Bio(request.Bio),
                request.AccountType,
                request.AccountCategory
            );

            Result result = await this.Sender.Send(command, cancellationToken);

            if (result.IsFailure && result.Error == UserErrors.NotAuthorized)
            {
                return this.Unauthorized(result.Error.Name);
            }

            return this.HandleResult(result);
        }
    }
}
