using System.Runtime.InteropServices;
using WebDriverDownloader.Model;

namespace WebDriverDownloader.Utils;

internal static class PlatformInfo
{
    private static IReadOnlyDictionary<string, Platform> _platformNames = new Dictionary<string, Platform>()
    {
        { "windows", Platform.Win64 },
        { "win", Platform.Win64 },
        { "win64", Platform.Win64 },
        { "win64bit", Platform.Win64 },

        { "win32", Platform.Win32 },
        { "win32bit", Platform.Win32 },

        { "mac", Platform.Mac64 },
        { "mac64", Platform.Mac64 },

        { "macArm", Platform.MacArm64 },
        { "macArm64", Platform.MacArm64 },
    };

    public static Platform ParsePlatform(string platformName)
    {
        platformName = platformName.Trim().ToLower();
        if (!_platformNames.ContainsKey(platformName))
        {
            var validPlatformNames = _platformNames
                .GroupBy(n => n.Value)
                .Select(g => $"{g.Key} : {string.Join(", ", g.Select(v => v.Key.ToString()))}");
            throw new ArgumentException($"Couldn't parse platform \"{platformName}\"\n, valid platforms are:\n{string.Join("\n", validPlatformNames)}");
        }
        return _platformNames[platformName];
    }

    public static Platform GetCurrentPlatform()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return RuntimeInformation.OSArchitecture switch
            {
                Architecture.X64 => Platform.Win64,
                Architecture.X86 => Platform.Win32,
                _ => throw new NotImplementedException($"Platform win-{RuntimeInformation.OSArchitecture} not found"),
            };
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return Platform.Linux64;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return RuntimeInformation.OSArchitecture switch
            {
                Architecture.X64 => Platform.Mac64,
                Architecture.Arm64 => Platform.MacArm64,
                _ => throw new NotImplementedException($"Platform macos-{RuntimeInformation.OSArchitecture} not found"),
            };

        throw new NotImplementedException($"Platform {RuntimeInformation.OSDescription}-{RuntimeInformation.OSArchitecture} not found");
    }
}
