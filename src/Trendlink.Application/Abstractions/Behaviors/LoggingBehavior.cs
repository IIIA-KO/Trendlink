using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Abstractions.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseRequest
        where TResponse : Result
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            this._logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            string name = request.GetType().Name;

            try
            {
                this._logger.LogInformation("Executing reqeust {Request}", name);

                TResponse result = await next();

                if (result.IsSuccess)
                {
                    this._logger.LogInformation("Request {Request} processed successfully", name);
                }
                else
                {
                    using (LogContext.PushProperty("Error", result.Error, true))
                    {
                        this._logger.LogError("Request {Request} processed with error", name);
                    }
                }

                return result;
            }
            catch (Exception exception)
            {
                this._logger.LogError(exception, "Request {Request} processing failed", name);

                throw new Exception($"Error processing request {name}",  exception);
            }
        }
    }
}
