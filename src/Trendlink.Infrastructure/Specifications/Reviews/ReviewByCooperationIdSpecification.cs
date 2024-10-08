﻿using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Reviews;

namespace Trendlink.Infrastructure.Specifications.Reviews
{
    internal sealed class ReviewByCooperationIdSpecification : Specification<Review, ReviewId>
    {
        public ReviewByCooperationIdSpecification(CooperationId cooperationId)
            : base(review => review.CooperationId == cooperationId) { }
    }
}
