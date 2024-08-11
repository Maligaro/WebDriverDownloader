using System.Runtime.InteropServices;
using WebDriverDownloader.Model;

namespace WebDriverDownloader.DriverDownloader;

internal interface IDriverUriResolver
{
    Uri GetDriverUri(DriverVersion driverVersion, Platform platform);
}
