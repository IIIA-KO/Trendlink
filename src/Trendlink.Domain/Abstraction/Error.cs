namespace Trendlink.Domain.Abstraction
{
    public record Error(string Code, string Name)
    {
        public static readonly Error None = new(string.Empty, string.Empty);

        public static readonly Error NullValue = new("Error.NullValue", "Null value was provided");

        public static readonly Error Unexpected =
            new("Error.Unexpected", "Unexpected error occured");
    }

    public record NotFoundError : Error
    {
        public NotFoundError(string code, string name)
            : base(code, name) { }
    }

    public record UnauthorizedError : Error
    {
        public UnauthorizedError(string code, string name)
            : base(code, name) { }
    }
}
