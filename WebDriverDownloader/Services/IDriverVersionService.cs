using WebDriverDownloader.Model;

namespace WebDriverDownloader.Services;

internal interface IDriverVersionService
{
    Task<DriverVersion> ResolveDriverVersionByBrowserVersion(BrowserVersion browserVersion);
}
