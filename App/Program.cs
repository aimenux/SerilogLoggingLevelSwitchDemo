using App.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace App;

public static class Program
{
    public static async Task Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();
        var service = host.Services.GetRequiredService<IDummyService>();
        await service.RunAsync();

        Console.WriteLine("Press any key to exit !");
        Console.ReadKey();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((_, config) =>
            {
                config.AddJsonFile();
                config.AddEnvironmentVariables();
                config.AddCommandLine(args);
            })
            .ConfigureLogging((hostingContext, loggingBuilder) =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddNonGenericLogger();
            })
            .ConfigureServices((_, services) =>
            {
                services.AddTransient<IDummyService, DummyService>();
                services.AddSingleton<ILoggingLevelSwitcher, LoggingLevelSwitcher>();
            })
            .UseSerilog()
            .UseConsoleLifetime();
}