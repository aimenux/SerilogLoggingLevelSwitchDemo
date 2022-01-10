using Serilog.Core;
using Serilog.Events;

namespace App;

public interface ILoggingLevelSwitcher
{
    LoggingLevelSwitch LevelSwitch { get; }

    void SetMinimumLevelFromConfiguration();

    void SetMinimumLevel(LogEventLevel level);
}