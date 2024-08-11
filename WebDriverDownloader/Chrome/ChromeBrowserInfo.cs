using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using WebDriverDownloader.Model;

namespace WebDriverDownloader.Chrome;

internal static class ChromeBrowserInfo
{
    public const int MinimalSupportedVersion = 114;
    public static Regex MilestoneVersionRegex = new Regex(@"(?:^| )(?<value>\d+)", RegexOptions.Compiled);
    public static Regex VersionRegex = new Regex(@"(?:^| )(?<value>[\d.]+)", RegexOptions.Compiled);
    public static Regex DriverNameRegex = new Regex("^chromedriver(.exe)?", RegexOptions.Compiled);

    public static IReadOnlyDictionary<Platform, string> PlatfomNames = new Dictionary<Platform, string>()
    {
        { Platform.Win64, "win64" },
        { Platform.Win32, "win32" },
        { Platform.Linux64, "linux64" },
        { Platform.Mac64, "mac-x64" },
        { Platform.MacArm64, "mac-arm64" },
    };
    public static IReadOnlySet<Platform> SupportedPlatforms { get; } = PlatfomNames.Keys.ToHashSet();
}
