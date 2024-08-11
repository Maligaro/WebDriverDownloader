using System.Runtime.InteropServices;
using WebDriverDownloader.Model;

namespace WebDriverDownloader.Utils
{
    internal static class Assert
    {
        public static void SystemIsSupported(
            Driver driver,
            Platform platform,
            IReadOnlySet<Platform> supportedSystems)
        {
            if (!supportedSystems.Contains(platform))
                throw new NotSupportedException($"System {platform} is not supported for driver {driver}");
        }
    }
}
