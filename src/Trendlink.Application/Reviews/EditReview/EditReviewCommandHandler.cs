using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Reviews;
using Trendlink.Domain.Shared;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Reviews.EditReview
{
    internal sealed class EditReviewCommandHandler : ICommandHandler<EditReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public EditReviewCommandHandler(
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
            EditReviewCommand request,
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

            if (this._userContext.UserId != review.BuyerId)
            {
                return Result.Failure(UserErrors.NotAuthorized);
            }

            Result<Rating> ratingResult = Rating.Create(request.Rating);
            if (ratingResult.IsFailure)
            {
                return ratingResult;
            }

            Result result = review.Update(ratingResult.Value, request.Comment);
            if (result.IsFailure)
            {
                return result;
            }

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
