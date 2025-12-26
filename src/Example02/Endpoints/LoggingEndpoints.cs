using Example02.Services;
using Serilog.Events;

namespace Example02.Endpoints;

public static class LoggingEndpoints
{
    public static void MapLoggingEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/logging/get-min-level", GetMinimumLogLevel)
            .WithName("GetMinimumLogLevel")
            .WithSummary("Gets the current minimum logging level")
            .WithDescription("Retrieves the current minimum logging level")
            .Produces<GetMinimumLogLevelResponse>();
        
        endpoints
            .MapPost("/logging/update-min-level", UpdateMinimumLogLevel)
            .WithName("UpdateMinimumLogLevel")
            .WithSummary("Changes the minimum logging level")
            .WithDescription("Changes the minimum logging level dynamically")
            .Produces(StatusCodes.Status200OK);
        
        endpoints
            .MapPost("/logging/log-to-all-levels", LogToAllLogLevels)
            .WithName("LogToAllLogLevels")
            .WithSummary("Logs a message to all logging levels")
            .WithDescription("Logs a message to all enabled logging levels")
            .Produces(StatusCodes.Status200OK);
    }
    
    private static IResult GetMinimumLogLevel(ILoggingLevelService loggingLevelService)
    {
        var minLevel = loggingLevelService.GetMinimumLevel().ToString();
        return Results.Ok(new GetMinimumLogLevelResponse(minLevel));
    }

    private static IResult UpdateMinimumLogLevel(ILoggingLevelService loggingLevelService, UpdateMinimumLogLevelRequest request)
    {
        if (!Enum.TryParse<LogEventLevel>(request.MinimumLogLevel, true, out var logEventLevel))
        {
            return Results.BadRequest($"Invalid log level '{request.MinimumLogLevel}'");
        }
        
        loggingLevelService.SetMinimumLevel(logEventLevel);
        return Results.Ok();
    }
    
    private static IResult LogToAllLogLevels(ILoggingLevelService loggingLevelService, ILoggingService loggingService)
    {
        var minLevel = loggingLevelService.GetMinimumLevel().ToString();
        loggingService.LogToAllLevels($"LoggingLevelSwitch >= {minLevel}");
        return Results.Ok();
    }
    
    private sealed record GetMinimumLogLevelResponse(string MinimumLogLevel);
    
    private sealed record UpdateMinimumLogLevelRequest(string MinimumLogLevel);
}