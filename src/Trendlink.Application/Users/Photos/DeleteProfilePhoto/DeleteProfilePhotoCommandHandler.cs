using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Photos;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.Photos.DeleteProfilePhoto
{
    internal sealed class DeleteProfilePhotoCommandHandler
        : ICommandHandler<DeleteProfilePhotoCommand>
    {
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProfilePhotoCommandHandler(
            IPhotoAccessor photoAccessor,
            IUserRepository userRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork
        )
        {
            this._photoAccessor = photoAccessor;
            this._userRepository = userRepository;
            this._userContext = userContext;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DeleteProfilePhotoCommand request,
            CancellationToken cancellationToken
        )
        {
            User user = await this._userRepository.GetByIdAsync(
                this._userContext.UserId,
                cancellationToken
            );

            if (user!.ProfilePhoto is not null)
            {
                Result deletePhotoResult = await this._photoAccessor.DeletePhotoAsync(
                    user.ProfilePhoto.Id
                );
                if (deletePhotoResult.IsFailure)
                {
                    return deletePhotoResult;
                }

                user.RemoveProfilePhoto();

                await this._unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            else
            {
                return Result.Failure(PhotoErrors.PhotoNotFound);
            }
        }
    }
}
