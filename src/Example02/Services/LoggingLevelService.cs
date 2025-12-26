using Serilog.Core;
using Serilog.Events;

namespace Example02.Services;

public interface ILoggingLevelService
{
    LoggingLevelSwitch LevelSwitch { get; }
    
    LogEventLevel GetMinimumLevel();
    
    void SetMinimumLevel(LogEventLevel level);
}

public sealed class LoggingLevelService : ILoggingLevelService
{
    public LoggingLevelSwitch LevelSwitch { get; }

    public LoggingLevelService(LoggingLevelSwitch levelSwitch)
    {
        LevelSwitch = levelSwitch;
    }

    public LogEventLevel GetMinimumLevel()
    {
        return LevelSwitch.MinimumLevel;
    }
    
    public void SetMinimumLevel(LogEventLevel level)
    {
        LevelSwitch.MinimumLevel = level;
    }
}