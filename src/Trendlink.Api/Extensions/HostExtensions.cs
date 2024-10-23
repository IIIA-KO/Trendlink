using Serilog;

namespace Trendlink.Api.Extensions
{
    public static class HostExtensions
    {
        public static void AddSerilogLogging(this ConfigureHostBuilder host)
        {
            host.UseSerilog(
                (context, loggerConfig) =>
                    loggerConfig.ReadFrom.Configuration(context.Configuration)
            );
        }
    }
}
