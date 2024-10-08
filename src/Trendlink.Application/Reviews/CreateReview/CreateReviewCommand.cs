﻿using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Reviews;

namespace Trendlink.Application.Reviews.CreateReview
{
    public sealed record CreateReviewCommand(
        int Rating,
        CooperationId CooperationId,
        Comment Comment
    ) : ICommand;
}
