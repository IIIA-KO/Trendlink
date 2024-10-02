using Microsoft.AspNetCore.Http;
using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Users.Photos.SetProfilePhoto
{
    public sealed record SetProfilePhotoCommand(IFormFile File) : ICommand;
}
