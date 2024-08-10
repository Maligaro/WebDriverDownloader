using System.Runtime.InteropServices;
using WebDriverDownloader.Model;

namespace WebDriverDownloader.Services;

internal interface IDriverUriResolver
{
    Uri GetDriverUri(DriverVersion driverVersion, OSPlatform platform, Architecture architecture);
}
