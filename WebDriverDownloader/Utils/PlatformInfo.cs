using System.Runtime.InteropServices;
namespace WebDriverDownloader.Utils;

internal static class PlatformInfo
{
    private static IReadOnlyDictionary<string, OSPlatform> _platformNames = new Dictionary<string, OSPlatform>()
    {
        { "windows", OSPlatform.Windows },
        { "win", OSPlatform.Windows },
        { "linux", OSPlatform.Linux },
        { "macos", OSPlatform.OSX },  
        { "osx", OSPlatform.OSX },  
    };
    
    public static OSPlatform ParsePlatform(string platform)
       => _platformNames[platform.Trim().ToLower()];

    public static OSPlatform GetCurrentPlatform()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return OSPlatform.Windows;
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return OSPlatform.Linux;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return OSPlatform.OSX;

        throw new NotSupportedException($"Operating system \"{RuntimeInformation.OSDescription}\" is not supported ");
    }
}
