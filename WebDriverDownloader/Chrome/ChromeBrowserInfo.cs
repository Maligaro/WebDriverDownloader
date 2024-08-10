using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace WebDriverDownloader.Chrome;

internal static class ChromeBrowserInfo
{
    public const int MinimalSupportedVersion = 114;
    public static Regex MilestoneVersionRegex = new Regex(@"(?:^| )(?<value>\d+)", RegexOptions.Compiled);
    public static Regex VersionRegex = new Regex(@"(?:^| )(?<value>[\d.]+)", RegexOptions.Compiled);
    public static Regex DriverNameRegex = new Regex("^chromedriver(.exe)?", RegexOptions.Compiled);

    public static IReadOnlyDictionary<(OSPlatform, Architecture), string> PlatfomNames = new Dictionary<(OSPlatform, Architecture), string>()
    {
        { (OSPlatform.Windows, Architecture.X64), "win64" },
        { (OSPlatform.Windows, Architecture.X86), "win32" },
        { (OSPlatform.Linux, Architecture.X64), "linux64" },
        { (OSPlatform.OSX, Architecture.X64), "mac-x64" },
        { (OSPlatform.OSX, Architecture.Arm64), "mac-arm64" },
    };
    public static IReadOnlySet<(OSPlatform, Architecture)> SupportedPlatforms { get; } = PlatfomNames.Keys.ToHashSet();
}
