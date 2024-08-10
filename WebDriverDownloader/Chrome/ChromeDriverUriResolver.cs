using System.Runtime.InteropServices;
using WebDriverDownloader.DriverDownloader;
using WebDriverDownloader.Model;

namespace WebDriverDownloader.Chrome;

internal class ChromeDriverUriResolver : IDriverUriResolver
{
    private const string _driverUrlTemplate = @"https://storage.googleapis.com/chrome-for-testing-public/@version/@platformName/chromedriver-@platformName.zip";

    public Uri GetDriverUri(DriverVersion driverVersion, OSPlatform platform, Architecture architecture)
    {
        if (!ChromeBrowserInfo.PlatfomNames.ContainsKey((platform, architecture)))
            throw new ArgumentException($"Coudn't find platform name for {platform}:{architecture}");

        var platformName = ChromeBrowserInfo.PlatfomNames[(platform, architecture)];
        var url = _driverUrlTemplate
            .Replace("@version", driverVersion.Value)
            .Replace("@platformName", platformName);

        return new Uri(url);
    }
}
