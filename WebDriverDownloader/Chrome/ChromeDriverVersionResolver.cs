using WebDriverDownloader.DriverDownloader;
using WebDriverDownloader.Model;
using WebDriverDownloader.Utils;

namespace WebDriverDownloader.Chrome;

internal class ChromeDriverVersionResolver : IDriverVersionService
{
    private const string _driverVersionApiUrlTemplate = @"https://googlechromelabs.github.io/chrome-for-testing/LATEST_RELEASE_@milestoneVersion";
    
    private string GetDriverVersionUrl(string milestoneVersion)
    {
        return _driverVersionApiUrlTemplate.Replace("@milestoneVersion", milestoneVersion);
    }

    public async Task<DriverVersion> ResolveDriverVersionByBrowserVersion(BrowserVersion browserVersion)
    {
        var milestoneVersion = ChromeBrowserInfo.MilestoneVersionRegex.Match(browserVersion.Value).Groups["value"].Value;
        var milestoneNumber = int.Parse(milestoneVersion);
        if (milestoneNumber < ChromeBrowserInfo.MinimalSupportedVersion)
            throw new NotSupportedException($"Only chrome versions above {ChromeBrowserInfo.MinimalSupportedVersion} are supported");

        var driverVersionUrl = GetDriverVersionUrl(milestoneVersion);
        var driverVersion = await Http.GetString(driverVersionUrl);
        return new DriverVersion(driverVersion);
    }
}