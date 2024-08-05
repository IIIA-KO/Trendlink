using System.Net.Http.Json;
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
using Trendlink.Api.FunctionalTests.Users;
using Trendlink.Application.Abstractions.Data;
using Trendlink.Application.Countries.GetAllCountries;
using Trendlink.Application.Countries.GetStates;
using Trendlink.Infrastructure;
using Trendlink.Infrastructure.Authentication;
using Trendlink.Infrastructure.Authentication.Keycloak;
using Trendlink.Infrastructure.Data;

namespace Trendlinl.Api.FunctionalTests.Infrastructure
{
    public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
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

                string connectionString =
                    $"{this._dbContainer.GetConnectionString()};Pooling=False";

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention()
                );

                services.RemoveAll(typeof(ISqlConnectionFactory));

                services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(
                    connectionString
                ));

                services.Configure<RedisCacheOptions>(redisCacheOptions =>
                    redisCacheOptions.Configuration = this._redisContainer.GetConnectionString()
                );

                string? keycloakAddress = this._keycloakContainer.GetBaseAddress();

                services.Configure<KeycloakOptions>(o =>
                {
                    o.AdminUrl = $"{keycloakAddress}admin/realms/trendlink/";
                    o.TokenUrl = $"{keycloakAddress}realms/trendlink/protocol/openid-connect/token";
                });

                services.Configure<AuthenticationOptions>(o =>
                {
                    o.Issuer = $"{keycloakAddress}realms/trendlink/";
                    o.MetadataUrl =
                        $"{keycloakAddress}realms/trendlink/.well-known/openid-configuration";
                });
            });
        }

        public async Task InitializeAsync()
        {
            await this._dbContainer.StartAsync();
            await this._redisContainer.StartAsync();
            await this._keycloakContainer.StartAsync();

            await this.InitializeTestUserAsync();
        }

        public new async Task DisposeAsync()
        {
            await this._dbContainer.StopAsync();
            await this._redisContainer.StopAsync();
            await this._keycloakContainer.StopAsync();
        }

        private async Task InitializeTestUserAsync()
        {
            try
            {
                using HttpClient httpClient = this.CreateClient();

                HttpResponseMessage response = await httpClient.PostAsJsonAsync(
                    "api/users/register",
                    UserData.RegisterTestUserRequest with
                    {
                        StateId = await this.GetValidStateId()
                    }
                );

                if (!response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(
                        $"User registration failed: {response.StatusCode}, {responseBody}"
                    );
                }
            }
            catch
            {
                // Do nothing.
            }
        }

        public async Task<Guid> GetValidStateId()
        {
            try
            {
                using HttpClient httpClient = this.CreateClient();

                HttpResponseMessage countryResponse = await httpClient.GetAsync("/api/countries");
                IReadOnlyList<CountryResponse> countries =
                    await countryResponse.Content.ReadFromJsonAsync<
                        IReadOnlyList<CountryResponse>
                    >();
                Guid countryId = countries!.First(c => c.Name == "United States").Id;

                HttpResponseMessage statesResponse = await httpClient.GetAsync(
                    $"https://localhost:5001/api/countries/{countryId}/states"
                );
                IReadOnlyList<StateResponse> states =
                    await statesResponse.Content.ReadFromJsonAsync<IReadOnlyList<StateResponse>>();

                return states![0].Id;
            }
            catch (Exception exception)
            {
                Console.WriteLine(
                    $"Exception was thrown during state id fetching.\n{exception.InnerException?.Message}"
                );
                throw;
            }
        }
    }
}
