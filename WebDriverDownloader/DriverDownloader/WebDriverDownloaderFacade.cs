using System.Runtime.InteropServices;
using WebDriverDownloader.Chrome;
using WebDriverDownloader.Model;

namespace WebDriverDownloader.DriverDownloader;

public class WebDriverDownloaderFacade
{
    private readonly IBrowserVersionDetector _browserVersionResolver;
    private readonly IDriverVersionService _driverVersionResolver;
    private readonly IDriverUriResolver _driverUriResolver;
    private readonly IDriverDownloader _driverDownloader;

    public WebDriverDownloaderFacade(Driver driver)
    {
        if (driver == Driver.Chromedriver)
        {
            _browserVersionResolver = new ChromeBrowserVersionDetector();
            _driverVersionResolver = new ChromeDriverVersionResolver();
            _driverDownloader = new ChromeDriverDownloader();
            _driverUriResolver = new ChromeDriverUriResolver();
        }
        //else if (driver == Driver.Geckodriver)
        //{
        //
        //}
        else
            throw new NotImplementedException();
    }


    public async Task DownloadDriver(string filePath, DriverVersion version, OSPlatform platform, Architecture architecture)
    {
        var uri = _driverUriResolver.GetDriverUri(version, platform, architecture);
        await _driverDownloader.DownloadDriver(uri, filePath);
    }

    public async Task<DriverVersion> GetBrowserMatchingDriverVersion()
    {
        var browserVersion = await _browserVersionResolver.GetInstalledBrowserVersion();
        var driverVersion = await _driverVersionResolver.ResolveDriverVersionByBrowserVersion(browserVersion);
        return driverVersion;
    }
}
