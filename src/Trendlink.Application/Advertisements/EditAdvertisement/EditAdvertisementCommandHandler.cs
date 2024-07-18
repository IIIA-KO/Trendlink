using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;

namespace Trendlink.Application.Advertisements.EditAdvertisement
{
    internal sealed class EditAdvertisementCommandHandler
        : ICommandHandler<EditAdvertisementCommand>
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IUserContext _userContext;
        private readonly IConditionRepository _conditionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditAdvertisementCommandHandler(
            IAdvertisementRepository advertisementRepository,
            IUserContext userContext,
            IConditionRepository conditionRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._advertisementRepository = advertisementRepository;
            this._userContext = userContext;
            this._conditionRepository = conditionRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            EditAdvertisementCommand request,
            CancellationToken cancellationToken
        )
        {
            Condition? condition =
                await this._conditionRepository.GetByUserIdWithAdvertisementAsync(
                    this._userContext.UserId,
                    cancellationToken
                );
            if (condition is null)
            {
                return Result.Failure<AdvertisementId>(ConditionErrors.NotFound);
            }

            Advertisement? advertisement = await this._advertisementRepository.GetByIdAsync(
                request.AdvertisementId,
                cancellationToken
            );
            if (advertisement is null)
            {
                return Result.Failure(AdvertisementErrors.NotFound);
            }

            if (condition.HasAdvertisement(request.Name))
            {
                return Result.Failure<AdvertisementId>(AdvertisementErrors.Duplicate);
            }

            Result result = advertisement.Update(request.Name, request.Price, request.Description);
            if (result.IsFailure)
            {
                return result;
            }

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
