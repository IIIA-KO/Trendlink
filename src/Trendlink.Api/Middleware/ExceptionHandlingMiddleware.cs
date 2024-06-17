using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ValidationException = Trendlink.Application.Exceptions.ValidationException;

namespace Trendlink.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private static readonly JsonSerializerOptions jsonSerializerOptions =
            new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IHostEnvironment environment
        )
        {
            this._next = next;
            this._logger = logger;
            this._environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch (Exception exception)
            {
                this._logger.LogError(
                    exception,
                    "Exception occurred: {Message}",
                    exception.Message
                );

                ExceptionDetails exceptionDetails = GetExceptionDetails(exception);

                var problemDetails = new ProblemDetails
                {
                    Status = exceptionDetails.Status,
                    Type = exceptionDetails.Type,
                    Title = exceptionDetails.Title,
                    Detail = exceptionDetails.Detail,
                };

                if (exceptionDetails.Errors is not null)
                {
                    problemDetails.Extensions["errors"] = exceptionDetails.Errors;
                }

                if (this._environment.IsDevelopment() && exceptionDetails.StackTrace is not null)
                {
                    problemDetails.Extensions["stackTrace"] = exceptionDetails.StackTrace;
                }

                context.Response.StatusCode = exceptionDetails.Status;

                string json = JsonSerializer.Serialize(context, jsonSerializerOptions);

                await context.Response.WriteAsync(json);
            }
        }

        private static ExceptionDetails GetExceptionDetails(Exception exception)
        {
            string stackTrace = exception.StackTrace?.ToString() ?? string.Empty;
            return exception switch
            {
                ValidationException validationException
                    => new ExceptionDetails(
                        StatusCodes.Status400BadRequest,
                        "ValidationFailure",
                        "Validation error",
                        "One or more validation errors has occurred",
                        stackTrace,
                        validationException.Errors
                    ),
                _
                    => new ExceptionDetails(
                        StatusCodes.Status500InternalServerError,
                        "ServerError",
                        "Server error",
                        "An unexpected error has occurred",
                        stackTrace,
                        null
                    )
            };
        }

        internal sealed record ExceptionDetails(
            int Status,
            string Type,
            string Title,
            string Detail,
            string? StackTrace,
            IEnumerable<object>? Errors
        );
    }
}
