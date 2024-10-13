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
using Trendlink.Application.Abstractions.Emails;
using Trendlink.Application.Abstractions.Instagram;
using Trendlink.Application.Abstractions.Photos;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Application.Abstractions.SignalR.Notifications;
using Trendlink.Domain.Abstraction;
using Trendlink.Infrastructure.Authentication;
using Trendlink.Infrastructure.Authentication.Google;
using Trendlink.Infrastructure.Authentication.Keycloak;
using Trendlink.Infrastructure.Authorization;
using Trendlink.Infrastructure.BackgroundJobs.EmailVerificationTokens;
using Trendlink.Infrastructure.BackgroundJobs.InstagramAccounts;
using Trendlink.Infrastructure.BackgroundJobs.Outbox;
using Trendlink.Infrastructure.BackgroundJobs.Token;
using Trendlink.Infrastructure.Caching;
using Trendlink.Infrastructure.Clock;
using Trendlink.Infrastructure.Data;
using Trendlink.Infrastructure.Emails;
using Trendlink.Infrastructure.Instagram;
using Trendlink.Infrastructure.Instagram.Abstraction;
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

            AddInstagramIntegration(services, configuration);

            AddAuthorization(services);

            AddEmail(services, configuration);

            AddCaching(services, configuration);

            AddBackgroundJobs(services, configuration);

            AddRealTimeServices(services);

            AddCloudinary(services, configuration);

            AddHealthChecks(services, configuration);

            return services;
        }

        private static void AddEmail(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailOptions>(configuration.GetSection("Email"));

            services
                .AddFluentEmail(configuration["Email:SenderEmail"], configuration["Email:Sender"])
                .AddSmtpSender(
                    configuration["Email:Host"],
                    configuration.GetValue<int>("Email:Port")
                );

            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IEmailVerificationLinkFactory, EmailVerificationLinkFactory>();
            services.AddHttpContextAccessor();
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
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<
                IEmailVerificationTokenRepository,
                EmailVerificationTokenRepository
            >();

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
        }

        private static void AddInstagramIntegration(
            IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.Configure<InstagramOptions>(configuration.GetSection("Instagram"));

            static void ConfigureHttpClient(IServiceProvider serviceProvider, HttpClient httpClient)
            {
                InstagramOptions instagramOptions = serviceProvider
                    .GetRequiredService<IOptions<InstagramOptions>>()
                    .Value;

                httpClient.BaseAddress = new Uri(instagramOptions.BaseUrl);
            }

            services.AddHttpClient<IFacebookService, FacebookService>(ConfigureHttpClient);

            services.AddHttpClient<IInstagramAccountsService, InstagramAccountsService>(
                ConfigureHttpClient
            );

            services.AddHttpClient<IInstagramPostsService, InstagramPostsService>(
                ConfigureHttpClient
            );

            services.AddScoped<IInstagramAudienceService, InstagramAudienceService>();

            services.AddHttpClient<IInstagramStatisticsService, InstagramStatiscticsService>(
                ConfigureHttpClient
            );

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
            services.ConfigureOptions<DeleteExpiredEmailVerificationTokensJobSetup>();
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

        private static void AddHealthChecks(
            IServiceCollection services,
            IConfiguration configuration
        )
        {
            services
                .AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("Database")!)
                .AddRedis(configuration.GetConnectionString("Cache")!)
                .AddUrlGroup(
                    new Uri(configuration["Keycloak:BaseUrl"]!),
                    HttpMethod.Get,
                    "keycloak"
                )
                .AddUrlGroup(new Uri(configuration["Email:BaseUrl"]!), HttpMethod.Get, "papercut");
        }
    }
}
