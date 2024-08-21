using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;

namespace Trendlink.Application.Advertisements.CreateAdvertisement
{
    internal sealed class CreateAdvertisementCommandHandler
        : ICommandHandler<CreateAdvertisementCommand, AdvertisementId>
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IUserContext _userContext;
        private readonly IConditionRepository _conditionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAdvertisementCommandHandler(
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

        public async Task<Result<AdvertisementId>> Handle(
            CreateAdvertisementCommand request,
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

            Result<Advertisement> result = Advertisement.Create(
                condition.Id,
                request.Name,
                request.Price,
                request.Description
            );
            if (result.IsFailure)
            {
                return Result.Failure<AdvertisementId>(result.Error);
            }

            Advertisement advertisement = result.Value;

            if (condition.HasAdvertisement(advertisement.Name))
            {
                return Result.Failure<AdvertisementId>(AdvertisementErrors.Duplicate);
            }

            this._advertisementRepository.Add(advertisement);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return advertisement.Id;
        }
    }
}
