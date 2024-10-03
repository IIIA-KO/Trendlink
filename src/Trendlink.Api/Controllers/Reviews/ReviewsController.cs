using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Reviews.CreateReview;
using Trendlink.Application.Reviews.EditReview;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Review;

namespace Trendlink.Api.Controllers.Reviews
{
    [Route("/api/reviews")]
    public class ReviewsController : BaseApiController
    {
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
    }
}
