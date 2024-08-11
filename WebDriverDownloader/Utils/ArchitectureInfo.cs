using System.Runtime.InteropServices;
namespace WebDriverDownloader.Utils;

internal static class ArchitectureInfo
{
    private static IReadOnlyDictionary<string, Architecture> _architectureNames = new Dictionary<string, Architecture>()
    {
        { "32", Architecture.X86 },
        { "32bit", Architecture.X86 },
        { "x32", Architecture.X86 },
        { "x32bit", Architecture.X86 },

        { "64", Architecture.X64 },
        { "64bit", Architecture.X64 },
        { "x64", Architecture.X64 },
        { "x64bit", Architecture.X64 },

        { "arm64", Architecture.Arm64 },
        { "arm", Architecture.Arm },
    };

    public static Architecture ParseArchitecture(string architecture)
    {
        architecture = architecture.Trim().ToLower();
        if (!_architectureNames.ContainsKey(architecture))
        {
            var validrAchitectures = string.Join(", ", _architectureNames.Select(n => $"\"{n}\""));
            throw new ArgumentException($"Couldn't find architecture \"{architecture}\", valid architecture names are {validrAchitectures}");
        }

        return _architectureNames[architecture];
    }

    public static Architecture GetCurrentArchitecture()
        => RuntimeInformation.OSArchitecture;
}
 