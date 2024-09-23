using FluentValidation;
using Microsoft.AspNetCore.Http;
using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Users.Photos.SetProfilePhoto
{
    public sealed record SetProfilePhotoCommand(IFormFile File) : ICommand;

    internal sealed class SetProfilePhotoCommandValidator
        : AbstractValidator<SetProfilePhotoCommand>
    {
        public SetProfilePhotoCommandValidator()
        {
            this.RuleFor(x => x.File)
                .NotNull()
                .WithMessage("File is required.")
                .Must(file => file.Length > 0)
                .WithMessage("Uploaded file cannot be empty.")
                .Must(file => IsSupportedFileType(file))
                .WithMessage("Only image files (jpg, jpeg, png) are supported.")
                .Must(file => file.Length <= 5 * 1024 * 1024)
                .WithMessage("File size must not exceed 5 MB.");
        }

        private static bool IsSupportedFileType(IFormFile file)
        {
            string[] supportedTypes = ["image/jpeg", "image/png", "image/jpg"];
            return supportedTypes.Contains(file.ContentType);
        }
    }
}
