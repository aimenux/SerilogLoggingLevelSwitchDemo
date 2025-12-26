using Example01.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;

namespace Example01.Extensions;

public static class SerilogExtensions
{
    public static IHostBuilder ConfigureSerilog(this IHostBuilder builder)
    {
        return builder.UseSerilog((hostingContext, serviceProvider, loggerConfiguration) =>
        {
            SelfLog.Enable(Console.Error);

            var loggingLevelService = serviceProvider.GetRequiredService<ILoggingLevelService>();
            var defaultLogLevel = hostingContext.Configuration.GetDefaultLogLevel();
            loggingLevelService.SetMinimumLevel(defaultLogLevel);
            
            var inputConfigType = hostingContext.Configuration.GetConfigType();
            var configType = ToConfigType(inputConfigType);
            switch (configType)
            {
                case ConfigType.FileBased:
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .MinimumLevel.ControlledBy(loggingLevelService.LevelSwitch);
                        
                    break;
                case ConfigType.CodeBased:
                    var filePath = hostingContext.Configuration.GetFilePath();
                    var outputTemplate = hostingContext.Configuration.GetOutputTemplate();
                    loggerConfiguration
                        .WriteTo.Console(outputTemplate: outputTemplate)
                        .WriteTo.File(filePath, rollingInterval: RollingInterval.Day)
                        .MinimumLevel.ControlledBy(loggingLevelService.LevelSwitch);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(inputConfigType);
            }
        });
    }
    
    private static ConfigType ToConfigType(string configType)
    {
        return Enum.TryParse<ConfigType>(configType, true, out var type) 
            ? type 
            : throw new ArgumentException($"Invalid config type {configType}");
    }

    private enum ConfigType
    {
        FileBased,    // Configuration from settings file
        CodeBased     // Configuration defined in C# code
    }
}