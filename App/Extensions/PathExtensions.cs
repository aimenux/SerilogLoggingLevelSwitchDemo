using System.Reflection;

namespace App.Extensions;

public static class PathExtensions
{
    public static string GetDirectoryPath()
    {
        try
        {
            return Path.GetDirectoryName(GetAssemblyLocation())!;
        }
        catch
        {
            return Directory.GetCurrentDirectory();
        }
    }

    private static string GetAssemblyLocation() => Assembly.GetExecutingAssembly().Location;
}