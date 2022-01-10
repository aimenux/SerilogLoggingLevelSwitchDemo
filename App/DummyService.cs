using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace App;

public class DummyService : IDummyService
{
    private readonly ILogger _logger;
    private readonly ILoggingLevelSwitcher _switcher;

    public DummyService(ILogger logger, ILoggingLevelSwitcher switcher)
    {
        _logger = logger;
        _switcher = switcher;
    }

    public Task RunAsync()
    {
        // only logs above information level will be shown
        LogToAllLevels("LoggingLevelSwitch >= Info");

        // let's enable now LoggingLevelSwitch with verbose
        _switcher.SetMinimumLevel(LogEventLevel.Verbose);

        // all logs above verbose level will be shown
        LogToAllLevels("LoggingLevelSwitch >= Verbose");

        // let's restore LoggingLevelSwitch to initial value
        _switcher.SetMinimumLevelFromConfiguration();

        // only logs above information level will be shown
        LogToAllLevels("LoggingLevelSwitch >= Info");

        return Task.CompletedTask;
    }

    private void LogToAllLevels(string message)
    {
        var scope = $"Scope-{message}";
        using (_logger.BeginScope(scope))
        {
            _logger.LogTrace(message);
            _logger.LogDebug(message);
            _logger.LogInformation(message);
            _logger.LogWarning(message);
            _logger.LogError(message);
            _logger.LogCritical(message);
        }
    }
}