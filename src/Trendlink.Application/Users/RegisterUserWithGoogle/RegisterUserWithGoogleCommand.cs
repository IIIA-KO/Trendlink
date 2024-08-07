﻿using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Users.LogInUser;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Users.RegisterUserWithGoogle
{
    public sealed record RegisterUserWithGoogleCommand(
        string Code,
        DateOnly BirthDate,
        PhoneNumber PhoneNumber,
        StateId StateId
    ) : ICommand<AccessTokenResponse>;
}
