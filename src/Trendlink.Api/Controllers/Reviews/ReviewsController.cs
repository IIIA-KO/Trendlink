using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Reviews.CreateReview;
using Trendlink.Application.Reviews.DeleteReview;
using Trendlink.Application.Reviews.EditReview;
using Trendlink.Application.Reviews.GetReview;
using Trendlink.Application.Reviews.GetUserReviews;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Reviews;
using Trendlink.Domain.Users;

namespace Trendlink.Api.Controllers.Reviews
{
    [Route("/api/reviews")]
    public class ReviewsController : BaseApiController
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetReview(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetReviewQuery(new ReviewId(id));

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetUserReviews(
            [FromRoute] Guid userId,
            [FromQuery] string? searchTerm,
            [FromQuery] int? rating,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default
        )
        {
            var query = new GetUserReviewsQuery(
                new UserId(userId),
                searchTerm,
                rating,
                pageNumber,
                pageSize
            );

            return this.HandlePagedResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpPost("{cooperationId:guid}")]
        public async Task<IActionResult> CreateReview(
            Guid cooperationId,
            [FromBody] CreateReviewRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new CreateReviewCommand(
                request.Rating,
                new CooperationId(cooperationId),
                new Comment(request.Comment)
            );

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> EditReview(
            [FromBody] EditReviewRequest request,
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var command = new EditReviewCommand(
                new ReviewId(id),
                request.Rating,
                new Comment(request.Comment)
            );

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteReview(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteReviewCommand(new ReviewId(id));

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }
    }
}
