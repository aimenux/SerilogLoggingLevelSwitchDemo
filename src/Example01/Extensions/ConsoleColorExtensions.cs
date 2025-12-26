namespace Example01.Extensions;

public static class ConsoleColorExtensions
{
    public static void WriteLine(this ConsoleColor color, object value)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(value);
        Console.ResetColor();
    }
}