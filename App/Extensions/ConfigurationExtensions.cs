using Microsoft.Extensions.Configuration;
using Serilog.Events;

namespace App.Extensions;

public static class ConfigurationExtensions
{
    public static string GetConfigType(this IConfiguration configuration)
    {
        return configuration["ConfigType"];
    }

    public static string GetOutputTemplate(this IConfiguration configuration)
    {
        return configuration["Serilog:WriteTo:0:Args:outputTemplate"];
    }

    public static string GetFilePath(this IConfiguration configuration)
    {
        return configuration["Serilog:WriteTo:1:Args:path"];
    }

    public static LogEventLevel GetDefaultLogLevel(this IConfiguration configuration)
    {
        return configuration.GetValue<LogEventLevel>("Serilog:MinimumLevel:Default");
    }
}