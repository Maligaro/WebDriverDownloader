using Microsoft.Win32;
using System.Diagnostics;
using WebDriverDownloader.DriverDownloader;
using WebDriverDownloader.Model;
using WebDriverDownloader.Utils;

namespace WebDriverDownloader.Chrome;

internal class ChromeBrowserVersionDetector : IBrowserVersionDetector
{
    private static readonly IReadOnlySet<Platform> _registryPlatforms = new HashSet<Platform>()
    {
        Platform.Win32,
        Platform.Win64
    };

    private static readonly IReadOnlySet<Platform> _terminalPlatforms = new HashSet<Platform>()
    {
        Platform.Linux64,
        Platform.Mac64,
        Platform.MacArm64,
    };

    public async Task<BrowserVersion> GetInstalledBrowserVersion()
    {
        var platform = PlatformInfo.GetCurrentPlatform();
        Assert.SystemIsSupported(Driver.Chromedriver, platform, ChromeBrowserInfo.SupportedPlatforms);

        string chromeVersion;
        if (_registryPlatforms.Contains(platform))
        {
            chromeVersion = GetChromeVersionFromRegistry();
        }
        else if (_terminalPlatforms.Contains(platform))
        {
            chromeVersion = await GetChromeVersionFromTerminal();
        }
        else
            throw new NotImplementedException();

        return new BrowserVersion(chromeVersion);
    }

    private const string _windowsRegistryChromePathForCurrentUser = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe";
    private const string _windowsRegistryChromePathFofAllUsers = @"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe";
    private string GetChromeVersionFromRegistry()
    {
        var pathEntry = Registry.GetValue(_windowsRegistryChromePathForCurrentUser, "", null) ??
            Registry.GetValue(_windowsRegistryChromePathFofAllUsers, "", null);

        if (pathEntry is null)
            throw new Exception($"Can't find Chrome browser path in registry entries \"{_windowsRegistryChromePathForCurrentUser}\", \"{_windowsRegistryChromePathFofAllUsers}\", ensure Google Chrome is installed");

        var path = pathEntry.ToString();
        var chromeVersion = FileVersionInfo.GetVersionInfo(path).FileVersion;
        return chromeVersion;
    }

    private async Task<string> GetChromeVersionFromTerminal()
    {
        var process = new Process();
        process.StartInfo.FileName = "google-chrome";
        process.StartInfo.Arguments = "/C --version"; 
        process.StartInfo.RedirectStandardOutput = true;

        process.Start();
        await process.WaitForExitAsync();
        var versionInfo = await process.StandardOutput.ReadToEndAsync();
        var fullVersion = ChromeBrowserInfo.VersionRegex.Match(versionInfo).Groups["value"].Value;
        return fullVersion;
    }
}
