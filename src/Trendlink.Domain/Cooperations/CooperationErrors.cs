﻿using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Cooperations
{
    internal static class CooperationErrors
    {
        public static readonly NotFoundError NotFound =
            new(
                "Cooperation.NotFound",
                "The cooperation with the specified identifier was not found"
            );

        public static readonly Error SameUser =
            new("Cooperation.SameUser", "The buyer and seller cannot be the same person");

        public static readonly Error NotPending =
            new("Cooperation.NotPending", "The cooperation is not pending");

        public static readonly Error NotConfirmed =
            new("Cooperation.NotConfirmed", "The cooperation is not confirmed");

        public static readonly Error AlreadyStarted =
            new("Cooperation.AlreadyStarted", "The cooperation has already started");
    }
}