using Example01.Extensions;
using Example01.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog.Events;

namespace Example01;

public static class Program
{
    public static void Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .AddServices()
            .Build();
        
        var loggingService = host.Services.GetRequiredService<ILoggingService>();
        
        var loggingLevelService = host.Services.GetRequiredService<ILoggingLevelService>();
        
        var logLevels = new[]
        {
            LogEventLevel.Verbose,
            LogEventLevel.Debug,
            LogEventLevel.Information,
            LogEventLevel.Warning,
            LogEventLevel.Error,
            LogEventLevel.Fatal
        };
        
        foreach (var level in logLevels)
        {
            ConsoleColor.Blue.WriteLine($"Set minimum level to {level}");
            loggingLevelService.SetMinimumLevel(level);
            loggingService.LogToAllLevels($"LoggingLevelSwitch >= {level}");
        }

        ConsoleColor.Yellow.WriteLine("Press any key to exit !");
        Console.ReadKey();
    }
}