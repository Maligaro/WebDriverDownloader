using WebDriverDownloader.DriverDownloader;
using WebDriverDownloader.Model;

namespace WebDriverDownloader.Chrome;

internal class ChromeDriverUriResolver : IDriverUriResolver
{
    private const string _driverUrlTemplate = @"https://storage.googleapis.com/chrome-for-testing-public/@version/@platformName/chromedriver-@platformName.zip";

    public Uri GetDriverUri(DriverVersion driverVersion, Platform platform)
    {
        if (!ChromeBrowserInfo.PlatfomNames.ContainsKey(platform))
            throw new ArgumentException($"Coudn't find platform name for {platform}");

        var platformName = ChromeBrowserInfo.PlatfomNames[platform];
        var url = _driverUrlTemplate
            .Replace("@version", driverVersion.Value)
            .Replace("@platformName", platformName);

        return new Uri(url);
    }
}
