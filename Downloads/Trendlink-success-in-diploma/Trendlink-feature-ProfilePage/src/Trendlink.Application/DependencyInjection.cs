using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Trendlink.Application.Abstractions.Behaviors;

namespace Trendlink.Application
{
    public static class DependencyInjection
    {
        private static readonly Assembly ApplicationAssembly = typeof(DependencyInjection).Assembly;

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(ApplicationAssembly);

                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));

                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

                configuration.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
            });

            services.AddValidatorsFromAssembly(ApplicationAssembly, includeInternalTypes: true);

            return services;
        }
    }
}
