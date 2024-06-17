using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Trendlink.Application.Abstractions.Caching;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Domain.Abstraction;
using Trendlink.Infrastructure.Caching;
using Trendlink.Infrastructure.Clock;
using Trendlink.Infrastructure.Data;
using Trendlink.Infrastructure.Outbox;

namespace Trendlink.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            AddPersistence(services, configuration);

            AddCaching(services, configuration);

            AddBackgroundJobs(services, configuration);

            return services;
        }

        private static void AddPersistence(
            IServiceCollection services,
            IConfiguration configuration
        )
        {
            string connectionString =
                configuration.GetConnectionString("Database")
                ?? throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseNpgsql(connectionString, options => options.EnableRetryOnFailure())
                    .UseSnakeCaseNamingConvention()
            );

            services.AddScoped<IUnitOfWork>(serviceProvider =>
                serviceProvider.GetRequiredService<ApplicationDbContext>()
            );

            services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(
                connectionString
            ));
        }

        private static void AddCaching(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString =
                configuration.GetConnectionString("Cache")
                ?? throw new ArgumentNullException(nameof(configuration));

            services.AddStackExchangeRedisCache(options =>
                options.Configuration = connectionString
            );

            services.AddSingleton<ICacheService, CacheService>();
        }

        private static void AddBackgroundJobs(
            IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

            services.AddQuartz();

            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
        }
    }
}
