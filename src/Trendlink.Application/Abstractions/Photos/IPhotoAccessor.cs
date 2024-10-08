using Microsoft.AspNetCore.Http;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Abstractions.Photos
{
    public interface IPhotoAccessor
    {
        Task<Result<Photo>> AddPhotoAsync(IFormFile file);

        Task<Result> DeletePhotoAsync(string photoId);
    }
}
