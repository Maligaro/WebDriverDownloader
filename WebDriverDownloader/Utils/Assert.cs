using System.Runtime.InteropServices;
using WebDriverDownloader.Model;

namespace WebDriverDownloader.Utils
{
    internal static class Assert
    {
        public static void SystemIsSupported(
            Driver driver,
            OSPlatform platform,
            Architecture architecture,
            IReadOnlySet<(OSPlatform, Architecture)> supportedSystems)
        {
            if (!supportedSystems.Contains((platform, architecture)))
                throw new NotSupportedException($"System {platform}:{architecture} is not supported for driver {driver}");
        }
    }
}
