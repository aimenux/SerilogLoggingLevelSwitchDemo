using Example02.Extensions;
using Example02.Services;
using Microsoft.AspNetCore.HttpLogging;
using Serilog.Core;

namespace Example02;

public static class DependencyInjection
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ILoggingService, LoggingService>();
        builder.Services.AddTransient<ILoggingLevelService, LoggingLevelService>();
        builder.Services.AddSingleton<LoggingLevelSwitch>(_ => new LoggingLevelSwitch());
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddHttpLogging();
        builder.Host.ConfigureSerilog();
        builder.AddOpenApi();
    }
    
    private static void AddHttpLogging(this IServiceCollection services)
    {
        services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;
            logging.CombineLogs = true;
        });
    }
}