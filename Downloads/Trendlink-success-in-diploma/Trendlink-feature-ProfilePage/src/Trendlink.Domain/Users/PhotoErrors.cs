using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users
{
    public static class PhotoErrors
    {
        public static readonly NotFoundError PhotoNotFound =
            new("Photo.NotFound", "User has no profile photo with provided indentifier");

        public static readonly Error FailedUpload =
            new("Photo.FailedUpload", "Error occured while uploading photo");

        public static readonly Error FailedDelete =
            new("Photo.FailedUpload", "Error occured while deleting photo");
    }
}
