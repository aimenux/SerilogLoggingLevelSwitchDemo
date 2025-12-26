using System.Reflection;
using Example01.Extensions;
using Example01.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog.Core;

namespace Example01;

public static class DependencyInjection
{
    public static IHostBuilder AddServices(this IHostBuilder builder)
    {
        return builder
            .ConfigureAppConfiguration((_, config) =>
            {
                config.SetBasePath(GetDirectoryPath());
                config.AddJsonFile("appsettings.json");
            })
            .ConfigureServices((_, services) =>
            {
                services.AddTransient<ILoggingService, LoggingService>();
                services.AddTransient<ILoggingLevelService, LoggingLevelService>();
                services.AddSingleton<LoggingLevelSwitch>(_ => new LoggingLevelSwitch());
            })
            .ConfigureSerilog();
    }
    
    private static string GetDirectoryPath()
    {
        try
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        }
        catch
        {
            return Directory.GetCurrentDirectory();
        }
    }
}