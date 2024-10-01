using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace Trendlink.Infrastructure.Instagram.Abstraction
{
    internal abstract class InstagramBaseService
    {
        protected readonly HttpClient _httpClient;
        protected readonly InstagramOptions _instagramOptions;

        protected InstagramBaseService(
            HttpClient httpClient,
            IOptions<InstagramOptions> instagramOptions
        )
        {
            this._httpClient = httpClient;
            this._instagramOptions = instagramOptions.Value;
        }

        protected async Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await this._httpClient.GetAsync(url, cancellationToken);
            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonSerializer.Deserialize<T>(content);
        }

        protected async Task<JsonElement> GetAsync(string url, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await this._httpClient.GetAsync(url, cancellationToken);
            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonDocument.Parse(content).RootElement;
        }

        protected string BuildUrl(string endpoint, Dictionary<string, string> parameters)
        {
            var sb = new StringBuilder(this._instagramOptions.BaseUrl + endpoint);
            sb.Append('?');
            sb.AppendJoin("&", parameters.Select(p => $"{p.Key}={p.Value}"));
            return sb.ToString();
        }
    }
}
