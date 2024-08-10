using WebDriverDownloader.Model;

namespace WebDriverDownloader.DriverDownloader;

internal interface IBrowserVersionDetector
{
    Task<BrowserVersion> GetInstalledBrowserVersion();
}
