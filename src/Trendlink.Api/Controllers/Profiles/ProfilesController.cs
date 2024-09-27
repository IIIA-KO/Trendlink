using Microsoft.AspNetCore.Mvc;
using Trendlink.Api.Controllers.Users;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Users.EditUser;
using Trendlink.Application.Users.Photos.DeleteProfilePhoto;
using Trendlink.Application.Users.Photos.SetProfilePhoto;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;

namespace Trendlink.Api.Controllers.Profiles
{
    [Route("profile")]
    public class ProfilesController : BaseApiController
    {
        private readonly IUserContext _userContext;

        public ProfilesController(IUserContext userContext)
        {
            this._userContext = userContext;
        }

        [HttpPost("photo")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> SetProfilePhoto(
            [FromForm] IFormFile photo,
            CancellationToken cancellationToken
        )
        {
            var command = new SetProfilePhotoCommand(photo);

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpDelete("photo")]
        public async Task<IActionResult> DeleteProfilePhoto(CancellationToken cancellationToken)
        {
            var command = new DeleteProfilePhotoCommand();

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(
            [FromBody] EditUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new EditUserCommand(
                this._userContext.UserId,
                new FirstName(request.FirstName),
                new LastName(request.LastName),
                request.BirthDate,
                new StateId(request.StateId),
                new Bio(request.Bio),
                request.AccountCategory
            );

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }
    }
}
