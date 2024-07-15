﻿using System.Text.Json.Serialization;

namespace Trendlink.Api.Controllers.Notifications
{
    public sealed record СreateNotificationRequest
    {
        [JsonRequired]
        public Guid UserId { get; init; }

        [JsonRequired]
        public int NotificationType { get; init; }

        public string Title { get; init; }

        public string Message { get; init; }
    }
}
