namespace Trendlink.Infrastructure.Outbox
{
    public sealed class OutboxMessage
    {
        public OutboxMessage(Guid id, DateTime occuredOnUtc, string type, string content)
        {
            this.Id = id;
            this.OccuredOnUtc = occuredOnUtc;
            this.Type = type;
            this.Content = content;
        }

        public Guid Id { get; init; }

        public DateTime OccuredOnUtc { get; init; }

        public string Type { get; init; }

        public string Content { get; init; }

        public DateTime? ProcessedOnUtc { get; init; }

        public string? Error { get; init; }
    }
}
