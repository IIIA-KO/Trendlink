namespace Trendlink.Application.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public ValidationException(IEnumerable<ValidationError> errors)
        {
            this.Errors = errors;
        }

        public IEnumerable<ValidationError> Errors { get; }
    }
}
