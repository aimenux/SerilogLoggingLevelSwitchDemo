using Serilog.Events;

namespace Example02.Extensions;

public static class ConfigurationExtensions
{
    extension(IConfiguration configuration)
    {
        public string GetConfigType()
        {
            return configuration["ConfigType"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GetOutputTemplate()
        {
            return configuration["Serilog:WriteTo:0:Args:outputTemplate"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GetFilePath()
        {
            return configuration["Serilog:WriteTo:1:Args:path"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public LogEventLevel GetDefaultLogLevel()
        {
            return configuration.GetValue<LogEventLevel>("Serilog:MinimumLevel:Default");
        }
    }
}