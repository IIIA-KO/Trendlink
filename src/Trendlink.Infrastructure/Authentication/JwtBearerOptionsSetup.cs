using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Trendlink.Infrastructure.Authentication
{
    internal sealed class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly AuthenticationOptions _authenticationOptions;

        public JwtBearerOptionsSetup(IOptions<AuthenticationOptions> authenticationOptions)
        {
            this._authenticationOptions = authenticationOptions.Value;
        }

        public void Configure(JwtBearerOptions options)
        {
            options.Audience = this._authenticationOptions.Audience;
            options.MetadataAddress = this._authenticationOptions.MetadataUrl;
            options.RequireHttpsMetadata = this._authenticationOptions.RequireHttpsMetadata;
            options.TokenValidationParameters.ValidIssuer = this._authenticationOptions.Issuer;
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            this.Configure(options);
        }
    }
}
