using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace Trendlink.Api.Middleware
{
    public class RequestContextLoggingMiddleware
    {
        private const string CorrelationIdHeaderName = "X-Correlation-Id";

        private readonly RequestDelegate _next;

        public RequestContextLoggingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public Task Invoke(HttpContext context)
        {
            using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
            {
                return this._next(context);
            }
        }

        private static string GetCorrelationId(HttpContext context)
        {
            context.Request.Headers.TryGetValue(
                CorrelationIdHeaderName,
                out StringValues correlationId
            );

            return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
        }
    }
}
