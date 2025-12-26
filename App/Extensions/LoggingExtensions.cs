using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App.Extensions;

public static class LoggingExtensions
{
    public static void AddLogging(this ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.ClearProviders();
        
        loggingBuilder.AddSimpleConsole(options =>
        {
            options.SingleLine = true;
            options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
        });
        
        loggingBuilder.Services.AddSingleton(serviceProvider =>
        {
            var categoryName = typeof(Program).Assembly.GetName().Name!;
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            return loggerFactory.CreateLogger(categoryName);
        });
    }
}