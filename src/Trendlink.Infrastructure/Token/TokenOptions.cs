namespace Trendlink.Infrastructure.Token
{
    internal sealed class TokenOptions
    {
        public int IntervalInSeconds { get; init; }

        public int BatchSize { get; init; }
    }
}
