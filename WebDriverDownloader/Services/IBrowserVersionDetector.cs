using WebDriverDownloader.Model;

namespace WebDriverDownloader.Services;

internal interface IBrowserVersionDetector
{
    Task<BrowserVersion> GetInstalledBrowserVersion();
}
