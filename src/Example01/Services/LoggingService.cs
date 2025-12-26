using Microsoft.Extensions.Logging;

namespace Example01.Services;

public interface ILoggingService
{
    void LogToAllLevels(string message);
}

public sealed class LoggingService : ILoggingService
{
    private readonly ILogger<LoggingService> _logger;

    public LoggingService(ILogger<LoggingService> logger)
    {
        _logger = logger;
    }

    public void LogToAllLevels(string message)
    {
        using var scope = _logger.BeginScope(nameof(LogToAllLevels));
        
        foreach (var level in Enum.GetValues<LogLevel>())
        {
            if (_logger.IsEnabled(level))
            {
                _logger.Log(level, "{level}: {message}", level, message);
            }
        }
    }
}