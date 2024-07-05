using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.Keycloak;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Infrastructure;
using Trendlink.Infrastructure.Authentication;
using Trendlink.Infrastructure.Data;

namespace Trendlink.Application.IntegrationTests.Infrastructure
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("trendlink")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();

        private readonly RedisContainer _redisContainer = new RedisBuilder()
            .WithImage("redis:latest")
            .Build();

        private readonly KeycloakContainer _keycloakContainer = new KeycloakBuilder()
            .WithResourceMapping(
                new FileInfo(".files/trendlink-realm-export.json"),
                new FileInfo("/opt/keycloak/data/import/realm.json")
            )
            .WithCommand("--import-realm")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

                string connectionString = $"{this._dbContainer.GetConnectionString()};Pooling=False";

                services.AddDbContext<ApplicationDbContext>(options =>
                    options
                        .UseNpgsql(connectionString)
                        .UseSnakeCaseNamingConvention());

                services.RemoveAll(typeof(ISqlConnectionFactory));

                services.AddSingleton<ISqlConnectionFactory>(_ =>
                    new SqlConnectionFactory(connectionString));

                services.Configure<RedisCacheOptions>(redisCacheOptions =>
                    redisCacheOptions.Configuration = this._redisContainer.GetConnectionString());

                string? keycloakAddress = this._keycloakContainer.GetBaseAddress();

                services.Configure<KeycloakOptions>(o =>
                {
                    o.AdminUrl = $"{keycloakAddress}admin/realms/bookify/";
                    o.TokenUrl = $"{keycloakAddress}realms/bookify/protocol/openid-connect/token";
                });
            });
        }

        public async Task InitializeAsync()
        {
            await this._dbContainer.StartAsync();
            await this._redisContainer.StartAsync();
            await this._keycloakContainer.StartAsync();
        }

        public new async Task DisposeAsync()
        {
            await this._dbContainer.StopAsync();
            await this._redisContainer.StopAsync();
            await this._keycloakContainer.StopAsync();
        }
    }
}
