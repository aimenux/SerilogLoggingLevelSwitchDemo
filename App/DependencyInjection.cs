using System.Reflection;
using App.Extensions;
using App.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog.Core;

namespace App;

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
            .ConfigureLogging((_, loggingBuilder) =>
            {
                loggingBuilder.AddLogging();
            })
            .UseSerilog()
            .UseConsoleLifetime();
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