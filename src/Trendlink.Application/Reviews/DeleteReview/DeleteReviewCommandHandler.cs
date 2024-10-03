using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Reviews;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Reviews.DeleteReview
{
    internal sealed class DeleteReviewCommandHandler : ICommandHandler<DeleteReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReviewCommandHandler(
            IReviewRepository reviewRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork
        )
        {
            this._reviewRepository = reviewRepository;
            this._userContext = userContext;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DeleteReviewCommand request,
            CancellationToken cancellationToken
        )
        {
            Review? review = await this._reviewRepository.GetByIdAsync(
                request.ReviewId,
                cancellationToken
            );
            if (review is null)
            {
                return Result.Failure(ReviewErrors.NotFound);
            }

            if (this._userContext.UserId != review!.BuyerId)
            {
                return Result.Failure(UserErrors.NotAuthorized);
            }

            this._reviewRepository.Remove(review);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
