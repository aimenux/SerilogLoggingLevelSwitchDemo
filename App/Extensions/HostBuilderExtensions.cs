using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;

namespace App.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseSerilog(this IHostBuilder builder)
    {
        return builder.UseSerilog((hostingContext, serviceProvider, loggerConfiguration) =>
        {
            SelfLog.Enable(Console.Error);

            var switcher = serviceProvider.GetRequiredService<ILoggingLevelSwitcher>();
            switcher.SetMinimumLevelFromConfiguration();

            var inputConfigType = hostingContext.Configuration.GetConfigType();
            var configType = ToConfigType(inputConfigType);
            switch (configType)
            {
                case ConfigType.Json:
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .MinimumLevel.ControlledBy(switcher.LevelSwitch);
                        
                    break;
                case ConfigType.Fluent:
                    var filePath = hostingContext.Configuration.GetFilePath();
                    var outputTemplate = hostingContext.Configuration.GetOutputTemplate();
                    loggerConfiguration
                        .WriteTo.Console(outputTemplate: outputTemplate)
                        .WriteTo.File(filePath, rollingInterval: RollingInterval.Day)
                        .MinimumLevel.ControlledBy(switcher.LevelSwitch);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(inputConfigType);
            }
        });
    }

    private static ConfigType ToConfigType(string configType)
    {
        if (Enum.TryParse<ConfigType>(configType, true, out var type))
        {
            return type;
        }

        throw new ArgumentException($"Invalid config type {configType}");
    }

    public enum ConfigType
    {
        Json,
        Fluent
    }
}