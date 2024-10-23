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

                if (this._environment.IsDevelopment())
                {
                    problemDetails.Extensions["stackTrace"] = exceptionDetails.StackTrace;
                    if (exceptionDetails.InnerExceptionDetails is not null)
                    {
                        problemDetails.Extensions["innerExceptionDetails"] =
                            exceptionDetails.InnerExceptionDetails;
                    }
                }

                context.Response.StatusCode = exceptionDetails.Status;
                context.Response.ContentType = "application/json";

                string json = JsonSerializer.Serialize(problemDetails, jsonSerializerOptions);

                await context.Response.WriteAsync(json);
            }
        }

        private static ExceptionDetails GetExceptionDetails(Exception exception)
        {
            var innerExceptionDetails = new List<InnerExceptionDetail>();
            Exception? currentException = exception.InnerException;

            while (currentException is not null)
            {
                innerExceptionDetails.Add(
                    new InnerExceptionDetail(currentException.Message, currentException.StackTrace)
                );
                currentException = currentException.InnerException;
            }

            string stackTrace = exception.StackTrace ?? string.Empty;

            return exception.InnerException switch
            {
                ValidationException validationException
                    => new ExceptionDetails(
                        StatusCodes.Status400BadRequest,
                        "ValidationFailure",
                        "Validation error",
                        "One or more validation errors has occurred",
                        stackTrace,
                        innerExceptionDetails,
                        validationException.Errors
                    ),
                _
                    => new ExceptionDetails(
                        StatusCodes.Status500InternalServerError,
                        "ServerError",
                        "Server error",
                        exception.Message,
                        stackTrace,
                        innerExceptionDetails,
                        null
                    )
            };
        }

        internal sealed record InnerExceptionDetail(string Message, string? StackTrace);

        internal sealed record ExceptionDetails(
            int Status,
            string Type,
            string Title,
            string Detail,
            string? StackTrace,
            IEnumerable<InnerExceptionDetail>? InnerExceptionDetails,
            IEnumerable<object>? Errors
        );
    }
}
