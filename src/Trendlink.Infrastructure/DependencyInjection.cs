using System.Configuration;
using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Caching;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Abstractions.Photos;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Abstractions.SignalR.Notifications;
using Trendlink.Domain.Abstraction;
using Trendlink.Infrastructure.Authentication;
using Trendlink.Infrastructure.Authentication.Google;
using Trendlink.Infrastructure.Authentication.Instagram;
using Trendlink.Infrastructure.Authentication.Keycloak;
using Trendlink.Infrastructure.Authorization;
using Trendlink.Infrastructure.BackgroundJobs.InstagramAccounts;
using Trendlink.Infrastructure.BackgroundJobs.Outbox;
using Trendlink.Infrastructure.BackgroundJobs.Token;
using Trendlink.Infrastructure.Caching;
using Trendlink.Infrastructure.Clock;
using Trendlink.Infrastructure.Data;
using Trendlink.Infrastructure.Photos;
using Trendlink.Infrastructure.Repositories;
using Trendlink.Infrastructure.SignalR;
using AuthenticationOptions = Trendlink.Infrastructure.Authentication.AuthenticationOptions;
using AuthenticationService = Trendlink.Infrastructure.Authentication.AuthenticationService;
using IAuthenticationService = Trendlink.Application.Abstractions.Authentication.IAuthenticationService;
using TokenOptions = Trendlink.Infrastructure.BackgroundJobs.Token.TokenOptions;

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

            AddAuthentication(services, configuration);

            AddAuthorization(services);

            AddCaching(services, configuration);

            AddBackgroundJobs(services, configuration);

            AddRealTimeServices(services);

            AddCloudinary(services, configuration);

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

            services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(
                connectionString
            ));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IConditionRepository, ConditionRepository>();
            services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
            services.AddScoped<ICooperationRepository, CooperationRepository>();
            services.AddScoped<IBlockedDateRepository, BlockedDateRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            services.AddScoped<IInstagramAccountRepository, InstagramAccountRepository>();

            services.AddScoped<IUnitOfWork>(serviceProvider =>
                serviceProvider.GetRequiredService<ApplicationDbContext>()
            );

            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        }

        private static void AddAuthentication(
            IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));

            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));

            services.Configure<GoogleOptions>(configuration.GetSection("Google"));

            services.Configure<InstagramOptions>(configuration.GetSection("Instagram"));

            services.AddTransient<AdminAuthorizationDelegatingHandler>();

            services
                .AddHttpClient<IAuthenticationService, AuthenticationService>(
                    (serviceProvider, httpClient) =>
                    {
                        KeycloakOptions keycloakOptions = serviceProvider
                            .GetRequiredService<IOptions<KeycloakOptions>>()
                            .Value;

                        httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
                    }
                )
                .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

            services.AddHttpClient<IKeycloakService, KeycloakService>(
                (serviceProvider, httpClient) =>
                {
                    KeycloakOptions keycloakOptions = serviceProvider
                        .GetRequiredService<IOptions<KeycloakOptions>>()
                        .Value;

                    httpClient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
                }
            );

            services.AddHttpContextAccessor();

            services.AddScoped<IUserContext, UserContext>();

            services.AddScoped<IGoogleService, GoogleService>();

            services.AddHttpClient<IInstagramService, InstagramService>(
                (serviceProvider, httpClient) =>
                {
                    InstagramOptions instagramOptions = serviceProvider
                        .GetRequiredService<IOptions<InstagramOptions>>()
                        .Value;

                    httpClient.BaseAddress = new Uri(instagramOptions.TokenUrl);
                }
            );
        }

        private static void AddAuthorization(IServiceCollection services)
        {
            services.AddScoped<AuthorizationService>();

            services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();
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
            services.Configure<TokenOptions>(configuration.GetSection("Token"));

            services.AddQuartz();

            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
            services.ConfigureOptions<CheckUserTokensJobSetup>();
            services.ConfigureOptions<UpdateInstagramAccountJobSetup>();
        }

        private static void AddRealTimeServices(IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
        }

        private static void AddCloudinary(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinaryOptions>(configuration.GetSection("Cloudinary"));

            services.AddSingleton<IPhotoAccessor, PhotoAccessor>();
        }
    }
}
