using System.Runtime.InteropServices;
namespace WebDriverDownloader.Utils;

internal static class ArchitectureInfo
{
    private static IReadOnlyDictionary<string, Architecture> _architectureNames = new Dictionary<string, Architecture>()
    {
        { "32", Architecture.X86 },
        { "32bit", Architecture.X86 },
        { "x32", Architecture.X86 },

        { "64", Architecture.X64 },
        { "64bit", Architecture.X64 },
        { "x64", Architecture.X64 },

        { "arm64", Architecture.Arm64 },
        { "arm", Architecture.Arm },
    };

    public static Architecture ParseArchitecture(string architecture)
       => _architectureNames[architecture.Trim().ToLower()];

    public static Architecture GetCurrentArchitecture()
        => RuntimeInformation.OSArchitecture;
}
