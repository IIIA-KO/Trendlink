using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.Advertisements;

namespace Trendlink.Application.Advertisements.DeleteAdvertisement
{
    internal sealed class DeleteAdvertisementCommandHandler
        : ICommandHandler<DeleteAdvertisementCommand>
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IConditionRepository _conditionRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAdvertisementCommandHandler(
            IAdvertisementRepository advertisementRepository,
            IConditionRepository conditionRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork
        )
        {
            this._advertisementRepository = advertisementRepository;
            this._conditionRepository = conditionRepository;
            this._userContext = userContext;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DeleteAdvertisementCommand request,
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

            this._advertisementRepository.Remove(advertisement);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
