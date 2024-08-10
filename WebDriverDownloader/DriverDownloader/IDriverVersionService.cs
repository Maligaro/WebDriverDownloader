using WebDriverDownloader.Model;

namespace WebDriverDownloader.DriverDownloader;

internal interface IDriverVersionService
{
    Task<DriverVersion> ResolveDriverVersionByBrowserVersion(BrowserVersion browserVersion);
}
